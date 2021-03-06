using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.semantic_model.types {

    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class VoidType : Type, IEquatable<VoidType> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

        public override string Name => "void";


        public VoidType() {
            ImmediateChildren = ImmutableArray.Create<ITreeNode>();
        }


        public override IType WithSubstitutedTypes(ISet<Substitution> typeMap) => this;


        public bool Equals(VoidType other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }


        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj?.GetType() != GetType()) return false;
            return Equals((VoidType) obj!);
        }


        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);

    }

}