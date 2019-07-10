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


		protected override bool IsAssignableToCore(Type type, HashSet<Error> errors) => true;


		public override bool IsAssignableToWithSubstitutions(Type type, ICollection<Substitution> substitutions, HashSet<Error> errors) {
			if (type is TypeParameterType t) {
				substitutions.Add(new Substitution(t, this));
			}
			return true;
		}


		public override bool IsEqualToWithSubstitutions(Type type, ICollection<Substitution> substitutions, HashSet<Error> errors) {
			if (type is TypeParameterType t) {
				substitutions.Add(new Substitution(t, this));
			}
			return true;
		}


		public override Type WithSubstitutedTypes(ISet<Substitution> typeMap) => this;


		public override string GetJsTypeCacheGetter() {
			throw new NotImplementedException();
		}


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