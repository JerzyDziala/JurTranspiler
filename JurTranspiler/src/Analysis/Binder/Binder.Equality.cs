using System.Collections.Generic;
using System.Linq;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.Analysis {

	public partial class Binder {

		private bool IsEqualToWithSubstitutions(IType self, IType type, ICollection<Substitution> substitutions) {
			return IsEqualToWithSubstitutionsCore((dynamic) self, (dynamic) type, substitutions);
		}


		private bool IsEqualToWithSubstitutionsCore(ArrayType self, IType type, ICollection<Substitution> substitutions) {
			if (type is TypeParameterType t) {
				substitutions.Add(new Substitution(t, self));
				return true;
			}
			if (type is ArrayType arrayType && IsEqualToWithSubstitutions(self.ElementType, arrayType.ElementType, substitutions)) return true;
			return true;
		}


		private bool IsEqualToWithSubstitutionsCore(FunctionPointerType self, IType type, ICollection<Substitution> substitutions) {
			if (type is TypeParameterType typeParameterType) {
				substitutions.Add(new Substitution(typeParameterType, self));
				return true;
			}
			if (type is FunctionPointerType func && IsEqualToWithSubstitutions(self.ReturnType, func.ReturnType, substitutions)) {
				if (self.Parameters.Length != func.Parameters.Length) return false;
				return func.Parameters.All((t, i) => IsEqualToWithSubstitutions(self.Parameters[i], t, substitutions));
			}
			return false;
		}


		private bool IsEqualToWithSubstitutionsCore(AnyType self, IType type, ICollection<Substitution> substitutions) {
			if (type is TypeParameterType t) {
				substitutions.Add(new Substitution(t, self));
				return true;
			}
			return type is AnyType;
		}


		private bool IsEqualToWithSubstitutionsCore(PrimitiveType self, IType type, ICollection<Substitution> substitutions) {
			if (type is TypeParameterType t) {
				substitutions.Add(new Substitution(t, self));
				return true;
			}
			return self.Equals(type);
		}


		private bool IsEqualToWithSubstitutionsCore(StructType self, IType type, ICollection<Substitution> substitutions) {
			if (type is TypeParameterType t) {
				substitutions.Add(new Substitution(t, self));
				return true;
			}
			if (type is StructType target) {
				var fieldsA = BindFields(self).ToList();
				var fieldsB = BindFields(target).ToList();
				var tmpSubs = new HashSet<Substitution>();

				if (fieldsA.Count == fieldsB.Count) {
					var isCompatible = fieldsB.All(fieldB => fieldsA.Any(fieldA => fieldA.Name.Equals(fieldB.Name)
					                                                            && IsEqualToWithSubstitutions(fieldA.Type, fieldB.Type, tmpSubs)));

					if (!isCompatible) return false;
					if (tmpSubs.GroupBy(x => x.typeParameter).Any(g => g.MoreThenOne())) return false;

					substitutions.AddRange(tmpSubs);
					return true;
				}
				return false;
			}
			return false;
		}


		private bool IsEqualToWithSubstitutionsCore(TypeParameterType self, IType type, ICollection<Substitution> substitutions) {
			if (type is TypeParameterType t) {
				substitutions.Add(new Substitution(t, self));
				return true;
			}
			return false;
		}


		private bool IsEqualToWithSubstitutionsCore(UndeclaredStructType self, IType type, ICollection<Substitution> substitutions) {
			if (type is UndeclaredStructType undeclaredType && undeclaredType.Name == self.Name) return true;
			if (type is TypeParameterType t) {
				substitutions.Add(new Substitution(t, self));
				return true;
			}
			return false;
		}


		private static bool IsEqualToWithSubstitutionsCore(UndefinedType self, IType type, ICollection<Substitution> substitutions) {
			if (type is TypeParameterType t) {
				substitutions.Add(new Substitution(t, self));
			}
			return true;
		}


		private bool IsEqualToWithSubstitutionsCore(VoidType self, IType type, ICollection<Substitution> substitutions) => false;

	}

}