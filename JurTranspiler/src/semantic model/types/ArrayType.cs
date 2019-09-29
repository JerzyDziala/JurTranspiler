using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.semantic_model.types {

    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class ArrayType : Type, IEquatable<ArrayType> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

        public override string Name => ElementType.Name + "[]";

        public IType ElementType { get; }


        public ArrayType(IType elementType) {
            ElementType = elementType;
            ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(ElementType);

        }


        public override IType WithSubstitutedTypes(ISet<Substitution> typeMap) {
            return new ArrayType(ElementType.WithSubstitutedTypes(typeMap));
        }


        public bool Equals(ArrayType other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Equals(ElementType, other.ElementType);
        }


        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj?.GetType() != GetType()) return false;
            return Equals((ArrayType) obj!);
        }


        public override int GetHashCode() {
            unchecked {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (ElementType != null ? ElementType.GetHashCode() : 0);
            }
        }

    }

}