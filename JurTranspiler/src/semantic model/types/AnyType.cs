using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.semantic_model {

    public class AnyType : Type, IEquatable<AnyType> {

        public override ImmutableList<ITreeNode> ImmediateChildren { get; }
        public override ImmutableList<ITreeNode> AllChildren { get; }
        public override string Name { get; }


        public AnyType() {
            Name = "any";
            ImmediateChildren = ImmutableList.Create<ITreeNode>();
            AllChildren = this.GetAllChildren();
        }


        public override Type WithSubstitutedTypes(ISet<Substitution> typeMap) => this;


        public bool Equals(AnyType other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }


        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AnyType) obj);
        }


        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);

    }

}