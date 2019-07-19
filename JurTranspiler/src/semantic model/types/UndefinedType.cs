using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.semantic_model {

	public class UndefinedType : Type, IEquatable<UndefinedType> {

		public override ImmutableList<ITreeNode> ImmediateChildren { get; }
		public override ImmutableList<ITreeNode> AllChildren { get; }
		public override string Name { get; }


		public UndefinedType() {
			Name = "undefined";
			ImmediateChildren = ImmutableList.Create<ITreeNode>();
			AllChildren = this.GetAllChildren();
		}







		public override Type WithSubstitutedTypes(ISet<Substitution> typeMap) => this;


		public bool Equals(UndefinedType other) {
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Name, other.Name);
		}


		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((UndefinedType) obj);
		}


		public override string GetDefaultValue() {
			throw new NotImplementedException();
		}


        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);

	}

}