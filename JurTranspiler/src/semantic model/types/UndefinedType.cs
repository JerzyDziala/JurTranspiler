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

        public override string GetJsTypeCacheGetter() {
            throw new NotImplementedException();
        }

		public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);

	}

}