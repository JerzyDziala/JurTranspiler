using System;
using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.types {

    public class VoidTypeSyntax : SyntaxNode, ITypeSyntax, IEquatable<VoidTypeSyntax> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }


        public string Name => "void";
        public string FullName => Name;


        public VoidTypeSyntax(ISyntaxNode parent, int line) : base(parent, line) {
            ImmediateChildren = ImmutableArray.Create<ITreeNode>();

        }


        public override string ToJs(Knowledge knowledge) {
            throw new NotImplementedException();
        }


        public bool Equals(VoidTypeSyntax other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }


        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj?.GetType() != GetType()) return false;
            return Equals((VoidTypeSyntax) obj!);
        }


        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);
    }

}