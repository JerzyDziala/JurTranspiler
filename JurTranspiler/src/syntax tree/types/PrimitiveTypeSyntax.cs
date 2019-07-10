using System;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.parsing.Implementations {

    public class PrimitiveTypeSyntax : ITypeSyntax, IEquatable<PrimitiveTypeSyntax> {

        public virtual ISyntaxNode Root { get; }
        public virtual ISyntaxNode Parent { get; }
        public virtual ImmutableList<ISyntaxNode> AllParents { get; }


        public virtual ImmutableList<ITreeNode> ImmediateChildren { get; }
        public virtual ImmutableList<ITreeNode> AllChildren { get; }
        public virtual string File { get; }
        public virtual int Line { get; }
        public virtual int Abstraction { get; }
        public virtual string Name { get; }
        public string FullName => Name;

        public string DefaultValue => Name == "num"
                                          ? "0"
                                          : Name == "string"
                                              ? "0"
                                              : "false";


        public PrimitiveTypeSyntax(ISyntaxNode parent, JurParser.PrimitiveTypeContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            Name = context.PRIMITIVE().GetText();
            ImmediateChildren = ImmutableList.Create<ITreeNode>();
            AllChildren = this.GetAllChildren();

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

        public string ToJs(Binder binder) => Name;

    }

}