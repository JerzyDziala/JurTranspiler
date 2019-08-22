using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using JurTranspiler.compilerSource.nodes;
using Type = JurTranspiler.syntax_tree.bases.Type;

namespace JurTranspiler.compilerSource.semantic_model {

    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class PrimitiveType : Type, IEquatable<PrimitiveType> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }


        public override string Name => PrimitiveKind.ToString().ToLower();

        public PrimitiveKind PrimitiveKind { get; }


        public PrimitiveType(PrimitiveKind primitiveKind) {
            PrimitiveKind = primitiveKind;
            ImmediateChildren = ImmutableArray.Create<ITreeNode>();

        }


        public override IType WithSubstitutedTypes(ISet<Substitution> typeMap) => this;


        public bool Equals(PrimitiveType other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && PrimitiveKind == other.PrimitiveKind;
        }


        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj?.GetType() != GetType()) return false;
            return Equals((PrimitiveType) obj!);
        }


        public override int GetHashCode() {
            unchecked {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (int) PrimitiveKind;
            }
        }

    }

}