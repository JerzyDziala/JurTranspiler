using System;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.src.syntax_tree.types {

    public class VoidTypeSyntax : SyntaxNode, ITypeSyntax, IEquatable<VoidTypeSyntax> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

        public string Name => "void";
        public string FullName => Name;


        public VoidTypeSyntax(ISyntaxNode parent, int line) : base(parent, line) {
            ImmediateChildren = ImmutableArray.Create<ITreeNode>();
            AllChildren = GetAllChildren();
        }


        public override string ToJs(Knowledge knowledge) {
            throw new NotImplementedException();
        }


        public bool Equals(VoidTypeSyntax other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }


        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((VoidTypeSyntax) obj);
        }


        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);
    }

}