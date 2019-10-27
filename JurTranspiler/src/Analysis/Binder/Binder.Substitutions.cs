using System.Collections.Generic;
using System.Linq;
using JurTranspiler.semantic_model;
using JurTranspiler.semantic_model.types;
using UtilityLibrary;

namespace JurTranspiler.Analysis.Binder {

	public partial class Binder {

		private bool IsAssignableToWithSubstitutions(IType self, IType type, ICollection<Substitution> substitutions) {
			
			if (type is TypeParameterType t) {
				substitutions.Add(new Substitution(t, self));
				return true;
			}

			if (type is AnyType && self.IsNot<VoidType>()) {
				return true;
			}

			if (self is UndefinedType || type is UndefinedType) {
				return true;
			}

			return IsAssignableToWithSubstitutionsCore((dynamic) self, (dynamic) type, substitutions);
		}


		private bool IsAssignableToWithSubstitutionsCore(ArrayType self, IType type, ICollection<Substitution> substitutions) {
			
			return type is ArrayType arrayType &&
			       IsAssignableToWithSubstitutions(self.ElementType, arrayType.ElementType, substitutions);
		}


		private bool IsAssignableToWithSubstitutionsCore(FunctionPointerType self, IType type, ICollection<Substitution> substitutions) {
			
			if (type is FunctionPointerType func) {

				//covariant
				if (!IsAssignableToWithSubstitutions(self.ReturnType, func.ReturnType, substitutions)) return false;

				if (self.Parameters.Length != func.Parameters.Length) return false;

				return self.Parameters
				           .Zip(func.Parameters, (sPar, tPar) => (sPar, tPar))
				           .All(x => {
					           if (x.tPar is TypeParameterType t) {
						           substitutions.Add(new Substitution(t, x.sPar));
						           return true;
					           }
					           //contravariant
					           return IsAssignableToWithSubstitutions(x.tPar, x.sPar, substitutions);
				           });
			}
			return false;
		}


		private bool IsAssignableToWithSubstitutionsCore(AnyType self, IType type, ICollection<Substitution> substitutions) {
			return !(type is VoidType);
		}


		private bool IsAssignableToWithSubstitutionsCore(PrimitiveType self, IType type, ICollection<Substitution> substitutions) {
			return self.Name == type.Name;
		}


		private bool IsAssignableToWithSubstitutionsCore(StructType self, IType type, ICollection<Substitution> substitutions) {
			
			if (type is StructType target) {
				
				var fieldsA = BindFields(self).ToList();
				var fieldsB = BindFields(target).ToList();
				
				var tmpSubs = new HashSet<Substitution>();

				var isCompatible = fieldsB.All(fieldB => fieldsA.Any(fieldA => fieldA.Name == fieldB.Name && IsAssignableToWithSubstitutions(fieldA.Type, fieldB.Type, tmpSubs)));

				if (!isCompatible) return false;
				if (tmpSubs.GroupBy(x => x.typeParameter).Any(g => g.MoreThenOne())) return false;
				
				substitutions.AddRange(tmpSubs);
				return true;
			}
			
			return false;
		}


		private bool IsAssignableToWithSubstitutionsCore(TypeParameterType self, IType type, ICollection<Substitution> substitutions) {
			return false;
		}


		private bool IsAssignableToWithSubstitutionsCore(UndeclaredStructType self, IType type, ICollection<Substitution> substitutions) {
			return type is UndeclaredStructType undeclaredType && undeclaredType.Name == self.Name;
		}


		private bool IsAssignableToWithSubstitutionsCore(UndefinedType self, IType type, ICollection<Substitution> substitutions) {
			return true;
		}


		private bool IsAssignableToWithSubstitutionsCore(VoidType self, IType type, ICollection<Substitution> substitutions) => type is VoidType;

	}

}