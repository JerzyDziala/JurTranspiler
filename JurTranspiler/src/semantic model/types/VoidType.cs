using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.semantic_model {

	public class VoidType : Type, IEquatable<VoidType> {

		public override ImmutableList<ITreeNode> ImmediateChildren { get; }
		public override ImmutableList<ITreeNode> AllChildren { get; }
		public override string Name { get; }


		public VoidType() {
			Name = "void";
			ImmediateChildren = ImmutableList.Create<ITreeNode>();
			AllChildren = this.GetAllChildren();
		}


		protected override bool IsAssignableToCore(Type type, HashSet<Error> errors) => type is VoidType;


		public override bool IsAssignableToWithSubstitutions(Type type, ICollection<Substitution> substitutions, HashSet<Error> errors) {
			return false;
		}


		public override bool IsEqualToWithSubstitutions(Type type, ICollection<Substitution> substitutions, HashSet<Error> errors) {
			return false;
		}


		public override Type WithSubstitutedTypes(ISet<Substitution> typeMap) => this;


		public override string GetDefaultValue() {
			throw new NotImplementedException();
		}


		public override string GetJsTypeCacheGetter() {
			throw new NotImplementedException();
		}


		public bool Equals(VoidType other) {
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Name, other.Name);
		}


		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((VoidType) obj);
		}


		public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);

	}

}