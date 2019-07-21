using System.Collections.Generic;
using System.Linq;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {

        private bool IsAssignableToWithSubstitutions(Type self, Type type, ICollection<Substitution> substitutions) {
            return IsAssignableToWithSubstitutionsCore((dynamic) self, (dynamic) type, substitutions);
        }


        private bool IsAssignableToWithSubstitutionsCore(ArrayType self, Type type, ICollection<Substitution> substitutions) {
            if (type is TypeParameterType t) {
                substitutions.Add(new Substitution(t, self));
                return true;
            }
            if (type is UndefinedType) return true;
            if (type is ArrayType arrayType && IsAssignableToWithSubstitutions(self.ElementType, arrayType.ElementType, substitutions)) return true;
            return true;
        }


        private bool IsAssignableToWithSubstitutionsCore(FunctionPointerType self, Type type, ICollection<Substitution> substitutions) {
            if (type is TypeParameterType typeParameterType) {
                substitutions.Add(new Substitution(typeParameterType, self));
                return true;
            }
            if (type is UndefinedType) return true;
            if (type is FunctionPointerType func && IsAssignableToWithSubstitutions(self.ReturnType, func.ReturnType, substitutions)) {
                if (self.Parameters.Count != func.Parameters.Count) return false;
                return self.Parameters
                           .Zip(func.Parameters, (aPar, bPar) => (aPar, bPar))
                           .All(x => {
                               if (x.bPar is TypeParameterType t) {
                                   substitutions.Add(new Substitution(t, x.aPar));
                                   return true;
                               }
                               return IsAssignableToWithSubstitutions(x.bPar, x.aPar, substitutions);
                           });
            }
            return false;
        }


        private bool IsAssignableToWithSubstitutionsCore(NullType self, Type type, ICollection<Substitution> substitutions) {
            if (type is TypeParameterType t) {
                substitutions.Add(new Substitution(t, self));
            }
            return true;
        }


        private bool IsAssignableToWithSubstitutionsCore(PrimitiveType self, Type type, ICollection<Substitution> substitutions) {
            if (IsAssignableTo(self, type)) return true;
            if (type is TypeParameterType t) {
                substitutions.Add(new Substitution(t, self));
                return true;
            }
            return false;
        }


        private bool IsAssignableToWithSubstitutionsCore(StructType self, Type type, ICollection<Substitution> substitutions) {
            if (type is UndefinedType) return true;
            if (type is TypeParameterType t) {
                substitutions.Add(new Substitution(t, self));
                return true;
            }
            if (type is StructType target) {
                var fieldsA = BindFields(self).ToList();
                var fieldsB = BindFields(target).ToList();
                var tmpSubs = new HashSet<Substitution>();

                var isCompatible = fieldsB.All(fieldB => fieldsA.Any(fieldA => IsEqualToWithSubstitutions(fieldA.Type, fieldB.Type, tmpSubs)
                                                                            && fieldA.Name.Equals(fieldB.Name)));

                if (!isCompatible) return false;
                else if (tmpSubs.GroupBy(x => x.typeParameter).Any(g => g.MoreThenOne())) return false;
                else {
                    substitutions.AddRange(tmpSubs);
                    return true;
                }
            }
            return false;
        }


        private bool IsAssignableToWithSubstitutionsCore(TypeParameterType self, Type type, ICollection<Substitution> substitutions) {
            if (type is UndefinedType) return true;
            if (type is TypeParameterType t) {
                substitutions.Add(new Substitution(t, self));
                return true;
            }
            return false;
        }


        private bool IsAssignableToWithSubstitutionsCore(UndeclaredStructType self, Type type, ICollection<Substitution> substitutions) {
            if (type is UndeclaredStructType undeclaredType && undeclaredType.Name == self.Name) return true;
            if (type is UndefinedType) return true;
            if (type is TypeParameterType t) {
                substitutions.Add(new Substitution(t, self));
                return true;
            }
            return false;
        }


        private bool IsAssignableToWithSubstitutionsCore(UndefinedType self, Type type, ICollection<Substitution> substitutions) {
            if (type is TypeParameterType t) {
                substitutions.Add(new Substitution(t, self));
            }
            return true;
        }


        private bool IsAssignableToWithSubstitutionsCore(VoidType self, Type type, ICollection<Substitution> substitutions) => type is VoidType;
    }

}