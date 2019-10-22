using System;
using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.types {

    public class ArrayTypeSyntax : SyntaxNode, ITypeSyntax, IEquatable<ArrayTypeSyntax> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

        public ITypeSyntax ElementType { get; }
        public string Name => ElementType.FullName + "[]";
        public string FullName => Name;


        public ArrayTypeSyntax(ISyntaxNode parent, JurParser.ArrayTypeContext context) : base(parent, context) {
            ElementType = ToType(context.type());
            ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(ElementType);
        }


        public override string ToJs(Knowledge knowledge) {
            throw new NotImplementedException();
        }


        public bool Equals(ArrayTypeSyntax other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Equals(ElementType, other.ElementType);
        }


        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj?.GetType() != GetType()) return false;
            return Equals((ArrayTypeSyntax) obj!);
        }


        public override int GetHashCode() {
            unchecked {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (ElementType != null ? ElementType.GetHashCode() : 0);
            }
        }
    }

}