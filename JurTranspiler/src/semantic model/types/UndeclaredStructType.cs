using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using JurTranspiler.compilerSource.nodes;
using Type = JurTranspiler.syntax_tree.bases.Type;

namespace JurTranspiler.compilerSource.semantic_model {

    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class UndeclaredStructType : Type, IEquatable<UndeclaredStructType> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

        public override string Name { get; }


        public UndeclaredStructType(string name) {
            Name = name;
            ImmediateChildren = ImmutableArray.Create<ITreeNode>();

        }


        public override IType WithSubstitutedTypes(ISet<Substitution> typeMap) => this;


        public bool Equals(UndeclaredStructType other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }


        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj?.GetType() != GetType()) return false;
            return Equals((UndeclaredStructType) obj!);
        }


        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);
    }

}