using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.InteropServices.WindowsRuntime;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.semantic_model {

    public class UndeclaredStructType : Type, IEquatable<UndeclaredStructType> {

        public override ImmutableList<ITreeNode> ImmediateChildren { get; }
        public override ImmutableList<ITreeNode> AllChildren { get; }


        public override string Name { get; }


        public UndeclaredStructType(string name) {
            Name = name;
            ImmediateChildren = ImmutableList.Create<ITreeNode>();
            AllChildren = this.GetAllChildren();
        }






        public override Type WithSubstitutedTypes(ISet<Substitution> typeMap) => this;


        public bool Equals(UndeclaredStructType other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }


        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UndeclaredStructType) obj);
        }


        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);
    }

}