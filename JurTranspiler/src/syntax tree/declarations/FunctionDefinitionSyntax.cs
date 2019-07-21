using System;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.src.syntax_tree.types;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

    public class FunctionDefinitionSyntax : ISyntaxNode, IStructOrFunctionDeclarationSyntax, IEquatable<FunctionDefinitionSyntax> {

        //INode
        public ISyntaxNode Root { get; }
        public ISyntaxNode Parent { get; }
        public ImmutableList<ISyntaxNode> AllParents { get; }
        public ImmutableList<ITreeNode> ImmediateChildren { get; }
        public ImmutableList<ITreeNode> AllChildren { get; }
        public string File { get; }
        public int Line { get; }
        public int Abstraction { get; }

        public string Name { get; }
        public string FullName { get; }
        public bool IsGeneric { get; }
        public int GenericArity { get; }
        public int Arity { get; }
        public bool IsExtern { get; }

        public ITypeSyntax ReturnType { get; }
        public ImmutableList<UninitializedVariableDeclarationSyntax> Parameters { get; }
        public ImmutableList<TypeParameterSyntax> TypeParametersInArgumentList { get; }
        public ImmutableList<TypeParameterSyntax> TypeParametersInGenericTypesList { get; }
        public ImmutableList<TypeParameterSyntax> TypeParameters { get; }
        public ImmutableList<(ITypeSyntax constrained, ITypeSyntax constrain)> Constraints { get; }
        public IStatementSyntax Body { get; }


        public FunctionDefinitionSyntax(ISyntaxNode parent, JurParser.FunctionDeclarationContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;
            Name = context.ID(0).GetText();
            IsExtern = context.EXTERN() != null;

            TypeParametersInGenericTypesList = context.ID()
                                                      .Skip(1)
                                                      .Select(x => new TypeParameterSyntax(this, x.GetText(), Line, this))
                                                      .ToImmutableList();
            TypeParameters = TypeParametersInGenericTypesList;

            Constraints = context.constraints()
                                 ?.constrain()
                                 .Select(x => (constrained: TypeSyntaxFactory.Create(this, x.type(0)),
                                               constrain: TypeSyntaxFactory.Create(this, x.type(1))))
                                 .ToImmutableList() ?? ImmutableList<(ITypeSyntax constrained, ITypeSyntax constrain)>.Empty;

            ReturnType = context.VOID() != null
                             ? new VoidTypeSyntax(this, Line)
                             : TypeSyntaxFactory.Create(this, context.type());

            Parameters = context.uninitializedVarDeclaration().Select(x => new UninitializedVariableDeclarationSyntax(this, x, true)).ToImmutableList();

            Arity = Parameters.Count;

            if (!IsExtern)
                Body = context.ARROW() == null
                           ? (IStatementSyntax) new BlockStatement(this, context.block())
                           : new ExpressionStatementSyntax(this, context.expression());

            TypeParametersInArgumentList = Parameters.SelectMany(x => x.AllChildren)
                                                     .OfType<TypeParameterSyntax>()
                                                     .ToImmutableList();

            GenericArity = TypeParameters.Select(x => x.Name).Distinct().Count();
            IsGeneric = GenericArity > 0;

            if (!IsGeneric) FullName = $"{ReturnType.Name} {Name}()";
            else FullName = $"{ReturnType.Name} {Name}<{string.Join(",", TypeParametersInGenericTypesList.Select(x => x.Name))}>({string.Join(",", Parameters.Select(x => x.Type.Name))})";

            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .Add(ReturnType)
                                             .AddRange(Parameters)
                                             .AddRange(Constraints.SelectMany(x => new[] {x.constrained, x.constrain}))
                                             .AddIfNotNull(Body);

            AllChildren = this.GetAllChildren();

        }


        public string ToJs(Knowledge knowledge) {
            if (IsExtern) return "";

            var name = knowledge.GetNewNameFor(this);
            string parameters = Parameters.Select(x => x.Name).Glue(", ");

            if (IsGeneric) {
                if (Arity > 0) parameters += ", ";
                parameters += "substitutions";
            }

            return $@"function {name}({parameters}) {Body.ToJs(knowledge)} ";
        }


        public bool Equals(FunctionDefinitionSyntax other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(File, other.File) && Line == other.Line && string.Equals(FullName, other.FullName);
        }


        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FunctionDefinitionSyntax) obj);
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