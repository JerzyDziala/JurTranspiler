using System;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;

namespace JurTranspiler.src.syntax_tree.types {

    public class ArrayTypeSyntax : ITypeSyntax, IEquatable<ArrayTypeSyntax> {

        public virtual ISyntaxNode Root { get; }
        public virtual ISyntaxNode Parent { get; }
        public virtual ImmutableList<ISyntaxNode> AllParents { get; }
        public virtual ImmutableList<ITreeNode> ImmediateChildren { get; }
        public virtual ImmutableList<ITreeNode> AllChildren { get; }
        public virtual string File { get; }
        public virtual int Line { get; }
        public virtual int Abstraction { get; }
        public virtual string Name { get; }

        public ITypeSyntax ElementType { get; }
        public string FullName => Name;
        public string DefaultValue => "null";


        public ArrayTypeSyntax(ISyntaxNode parent, JurParser.ArrayTypeContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            ElementType = TypeSyntaxFactory.Create(parent, context.type());
            Name = ElementType.FullName + "[]";
            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .Add(ElementType);
            AllChildren = this.GetAllChildren();

        }

        public string ToJs(Knowledge knowledge) {
            throw new NotImplementedException();
        }


        public bool Equals(ArrayTypeSyntax other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Equals(ElementType, other.ElementType);
        }


        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ArrayTypeSyntax) obj);
        }


        public override int GetHashCode() {
            unchecked {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (ElementType != null ? ElementType.GetHashCode() : 0);
            }
        }
    }

}