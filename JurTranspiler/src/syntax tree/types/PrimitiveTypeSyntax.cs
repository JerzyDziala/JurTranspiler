using System;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.parsing.Implementations {

    public class PrimitiveTypeSyntax : SyntaxNode, ITypeSyntax, IEquatable<PrimitiveTypeSyntax> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

        public virtual string Name { get; }
        public string FullName => Name;


        public PrimitiveTypeSyntax(ISyntaxNode parent, JurParser.PrimitiveTypeContext context) : base(parent, context) {
            Name = context.PRIMITIVE().GetText();
            ImmediateChildren = ImmutableArray.Create<ITreeNode>();
            AllChildren = GetAllChildren();
        }


        public bool Equals(PrimitiveTypeSyntax other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }


        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PrimitiveTypeSyntax) obj);
        }


        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);

        public override string ToJs(Knowledge knowledge) => Name;

    }

}