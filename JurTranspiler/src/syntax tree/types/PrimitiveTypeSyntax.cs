using System;
using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.types {

    public class PrimitiveTypeSyntax : SyntaxNode, ITypeSyntax, IEquatable<PrimitiveTypeSyntax> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }


        public virtual string Name { get; }
        public string FullName => Name;


        public PrimitiveTypeSyntax(ISyntaxNode parent, JurParser.PrimitiveTypeContext context) : base(parent, context) {
            Name = context.PRIMITIVE().GetText();
            ImmediateChildren = ImmutableArray.Create<ITreeNode>();

        }


        public bool Equals(PrimitiveTypeSyntax other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }


        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj?.GetType() != GetType()) return false;
            return Equals((PrimitiveTypeSyntax) obj!);
        }


        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);

        public override string ToJs(Knowledge knowledge) => Name;

    }

}