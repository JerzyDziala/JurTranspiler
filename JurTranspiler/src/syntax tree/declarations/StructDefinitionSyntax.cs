using System;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;
using JurTranspiler.syntax_tree.types;

namespace JurTranspiler.syntax_tree.declarations {

    public class StructDefinitionSyntax : SyntaxNode, IStructOrFunctionDeclarationSyntax, IEquatable<StructDefinitionSyntax> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

        public string Name { get; }
        public string FullName { get; }
        public bool IsGeneric => GenericArity > 0;
        public bool IsPrivate => false;
        public bool IsNominal { get; }
        public int GenericArity => TypeParameters.Length;
        public ImmutableArray<TypeParameterSyntax> TypeParameters { get; }
        public ImmutableArray<ITypeSyntax> InlinedTypes { get; }
        public ImmutableArray<FieldDeclarationSyntax> Fields { get; }


        public StructDefinitionSyntax(ISyntaxNode parent, JurParser.StructDeclarationContext context) : base(parent, context) {

            Name = context.ID(0).GetText();
            IsNominal = context.NOMINAL() != null;
            TypeParameters = context.ID().Skip(1).Select(x => new TypeParameterSyntax(this, x.GetText(), Line, this)).ToImmutableArray();
            InlinedTypes = ToTypes(context.inlinedType().Select(x => x.type()).ToArray());
            Fields = ToFields(context.uninitializedVarDeclaration());

            FullName = IsGeneric
                           ? Name + "<" + string.Join(",", TypeParameters.Select(x => x.Name)) + ">"
                           : Name;

            ImmediateChildren = ImmutableArray.Create<ITreeNode>()
                                              .AddRange(InlinedTypes)
                                              .AddRange(Fields);

        }


        public override string ToJs(Knowledge knowledge) {
            throw new Exception("type constructors should be generated from closed types");
        }


        public bool Equals(StructDefinitionSyntax other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(File, other.File) && Line == other.Line && string.Equals(FullName, other.FullName);
        }


        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj?.GetType() != GetType()) return false;
            return Equals((StructDefinitionSyntax) obj!);
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