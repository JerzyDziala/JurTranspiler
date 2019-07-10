using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.semantic_model {

	public class ArrayType : Type, IEquatable<ArrayType> {



		public override ImmutableList<ITreeNode> ImmediateChildren { get; }
		public override ImmutableList<ITreeNode> AllChildren { get; }
		public override string Name { get; }

		public Type ElementType { get; }


		public ArrayType(Type elementType) {
			ElementType = elementType;
			Name = elementType.Name + "[]";
			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .Add(ElementType);
			AllChildren = this.GetAllChildren();
		}


		protected override bool IsAssignableToCore(Type type, HashSet<Error> errors) {
			if (type is UndefinedType) return true;
			if (type is ArrayType arrayType && ElementType.IsAssignableTo(arrayType.ElementType, errors)) return true;
			return false;
		}

        public override bool IsAssignableToWithSubstitutions(Type type, ICollection<Substitution> substitutions, HashSet<Error> errors) {
            if (type is TypeParameterType t) {
                substitutions.Add(new Substitution(t, this));
                return true;
            }
			if (type is UndefinedType) return true;
			if (type is ArrayType arrayType && ElementType.IsAssignableToWithSubstitutions(arrayType.ElementType,substitutions, errors)) return true;
            return true;
        }


        public override bool IsEqualToWithSubstitutions(Type type, ICollection<Substitution> substitutions, HashSet<Error> errors) {
            if (type is TypeParameterType t) {
                substitutions.Add(new Substitution(t, this));
                return true;
            }
			if (type is ArrayType arrayType && ElementType.IsEqualToWithSubstitutions(arrayType.ElementType,substitutions, errors)) return true;
            return true;
        }


        public override Type WithSubstitutedTypes(ISet<Substitution> typeMap) {
			return new ArrayType(ElementType.WithSubstitutedTypes(typeMap));
		}

		public override string GetJsTypeCacheGetter() {
			return $"getCachedArrayType({ElementType.GetJsTypeCacheGetter()})";
		}



		public bool Equals(ArrayType other) {
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Name, other.Name) && Equals(ElementType, other.ElementType);
		}


		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ArrayType) obj);
		}


		public override int GetHashCode() {
			unchecked {
				return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (ElementType != null ? ElementType.GetHashCode() : 0);
			}
		}

	}

}