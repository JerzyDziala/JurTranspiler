using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.semantic_model {

	public class NullType : Type, IEquatable<NullType> {

		public override ImmutableList<ITreeNode> ImmediateChildren { get; }
		public override ImmutableList<ITreeNode> AllChildren { get; }

		public override string Name { get; }


		public NullType() {
			Name = "null";
			ImmediateChildren = ImmutableList.Create<ITreeNode>();
			AllChildren = ImmutableList.Create<ITreeNode>();
		}







		public override Type WithSubstitutedTypes(ISet<Substitution> typeMap) => this;


        public bool Equals(NullType other) {
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Name, other.Name);
		}


		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((NullType) obj);
		}


		public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);

	}

}