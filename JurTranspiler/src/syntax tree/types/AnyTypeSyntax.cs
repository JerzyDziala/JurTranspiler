using System;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;

namespace JurTranspiler.syntax_tree.types {

    public class AnyTypeSyntax : ITypeSyntax, IEquatable<AnyTypeSyntax> {

        public ISyntaxNode Root { get; }
        public ISyntaxNode Parent { get; }
        public ImmutableList<ITreeNode> ImmediateChildren { get; }
        public ImmutableList<ITreeNode> AllChildren { get; }
        public ImmutableList<ISyntaxNode> AllParents { get; }
        public string File { get; }
        public int Line { get; }
        public int Abstraction { get; }
        public string Name { get; }
        public string FullName { get; }
        public string DefaultValue { get; }


        public AnyTypeSyntax(ISyntaxNode parent, JurParser.AnyTypeContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            Name = "any";
            FullName = "any";
            DefaultValue = "null";

            ImmediateChildren = ImmutableList.Create<ITreeNode>();
            AllChildren = this.GetAllChildren();
        }


        public string ToJs(Knowledge knowledge) => throw new System.NotImplementedException();


        public bool Equals(AnyTypeSyntax other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }


        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AnyTypeSyntax) obj);
        }


        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);
    }

}