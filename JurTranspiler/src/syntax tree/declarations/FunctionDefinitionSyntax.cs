using System;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.src.syntax_tree.types;
using JurTranspiler.syntax_tree.bases;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

    public class FunctionDefinitionSyntax : SyntaxNode, IStructOrFunctionDeclarationSyntax, IEquatable<FunctionDefinitionSyntax> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

        public string Name { get; }

        private string parametersListString => string.Join(",", Parameters.Select(x => x.Type!.FullName));
        private string typeParametersListString => string.Join(",", TypeParameters.Select(x => x.FullName));

        public string FullName => !IsGeneric
                                      ? $"{ReturnType.FullName} {Name}({parametersListString})"
                                      : $"{ReturnType.FullName} {Name}<{typeParametersListString}>({parametersListString})";

        public bool IsGeneric => GenericArity > 0;
        public int GenericArity => TypeParameters.Length;
        public int Arity => Parameters.Length;
        public bool IsExtern { get; }

        public ITypeSyntax ReturnType { get; }
        public ImmutableArray<UninitializedVariableDeclarationSyntax> Parameters { get; }
        public ImmutableArray<TypeParameterSyntax> TypeParameters { get; }
        public ImmutableArray<(ITypeSyntax constrained, ITypeSyntax constrain)> Constraints { get; }
        public IStatementSyntax? Body { get; }


        public FunctionDefinitionSyntax(ISyntaxNode parent, JurParser.FunctionDeclarationContext context) : base(parent, context) {

            Name = context.ID(0).GetText();
            IsExtern = context.EXTERN() != null;

            TypeParameters = context.ID().Skip(1)
                                    .Select(x => new TypeParameterSyntax(parent: this,
                                                                         name: x.GetText(),
                                                                         line: Line,
                                                                         originalDeclarer: this))
                                    .ToImmutableArray();

            Constraints = context.constraints()?.constrain()
                                 .Select(x => (constrained: ToType(x.type(0)), constrain: ToType(x.type(1))))
                                 .ToImmutableArray() ?? ImmutableArray<(ITypeSyntax constrained, ITypeSyntax constrain)>.Empty;

            ReturnType = context.VOID()?.ToVoidType(this) ?? ToType(context.type());
            Parameters = ToUninitializedVariablesDefinitions(context.uninitializedVarDeclaration(), true);

            if (!IsExtern) {
                Body = context.ARROW() == null
                           ? (IStatementSyntax) new BlockStatement(this, context.block())
                           : new ExpressionStatementSyntax(this, context.expression());
            }

            ImmediateChildren = ImmutableArray.Create<ITreeNode>()
                                              .Add(ReturnType)
                                              .AddRange(Parameters)
                                              .AddRange(Constraints.SelectMany(x => new[] {x.constrained, x.constrain}))
                                              .AddIfNotNull(Body);

        }


        public override string ToJs(Knowledge knowledge) {
            if (IsExtern) return "";

            var name = knowledge.GetNewNameFor(this);
            var parameters = Parameters.Select(knowledge.GetNewNameFor).Glue(", ");

            if (IsGeneric) {
                if (Arity > 0) parameters += ", ";
                parameters += "_s_";
            }

            return $@"function {name}({parameters}) {Body?.ToJs(knowledge) ?? ""} ";
        }


        public bool Equals(FunctionDefinitionSyntax other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(File, other.File) && Line == other.Line && string.Equals(FullName, other.FullName);
        }


        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj?.GetType() != GetType()) return false;
            return Equals((FunctionDefinitionSyntax) obj!);
        }


        public override int GetHashCode() {
            unchecked {
                var hashCode = (File != null ? File.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Line;
                hashCode = (hashCode * 397) ^ (FullName != null ? FullName.GetHashCode() : 0);
                return hashCode;
            }
        }

    }

}