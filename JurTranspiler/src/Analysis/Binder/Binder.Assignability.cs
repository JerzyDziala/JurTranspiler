using System.Linq;
using JurTranspiler.semantic_model.types;
using UtilityLibrary;

namespace JurTranspiler.Analysis.Binder {

    public partial class Binder {


        public bool IsAssignableTo(IType a, IType b) {
            return symbols.AlreadyBound((a, b))
                       ? symbols.GetBindingFor((a, b))
                       : symbols.MakeBindingFor((a, b), IsAssignableToShared((dynamic) a, (dynamic)b));
        }

        private bool IsAssignableToShared(IType self, IType type) {
            if (self is UndefinedType || type is UndefinedType) {
                return true;
            }
            if (type is AnyType && self.IsNot<VoidType>()) {
                return true;
            }

            return IsAssignableToCore((dynamic) self, (dynamic) type);
        }

        private bool IsAssignableToCore(ArrayType self, IType type) {
            return type is ArrayType arrayType 
                   && IsAssignableTo(self.ElementType, arrayType.ElementType);
        }


        private bool IsAssignableToCore(FunctionPointerType self, IType type) {
            if (type is FunctionPointerType func) {
                
                //covariant
                if (!IsAssignableTo(self.ReturnType, func.ReturnType)) return false;
                
                if (self.Parameters.Length != func.Parameters.Length) return false;
                
				return self.Parameters
				           .Zip(func.Parameters, (sPar, tPar) => (sPar, tPar))
                           //contravariant
				           .All(x => IsAssignableTo(x.tPar, x.sPar));
            }
            return false;
        }




        private bool IsAssignableToCore(PrimitiveType self, IType type) {
			return self.Name == type.Name;
        }


        private bool IsAssignableToCore(AnyType self, IType type) {
            return !(type is VoidType);
        }


        private bool IsAssignableToCore(StructType self, IType type) {
            if (type is StructType target) {
                if (self.Name == type.Name) return true;

                if (target.IsNominal) {
                    return self.InheritsFrom(target);
                }
                
                var fieldsA = BindFields(self);
                var fieldsB = BindFields(target);
                return fieldsB.All(fieldB => fieldsA.Any(fieldA => fieldA.Name == fieldB.Name && IsAssignableTo(fieldA.Type,fieldB.Type)));
            }
            return false;
        }


        private bool IsAssignableToCore(TypeParameterType self, IType type) {
            if (type is TypeParameterType t1) {
                if (t1.Name == self.Name) return true;
                if (self.GetAllConstraints().Any(c => c.Equals(t1))) return true;
            }
            return false;
        }


        private bool IsAssignableToCore(UndeclaredStructType self, IType type) {
            return type is UndeclaredStructType undeclaredType && undeclaredType.Name == self.Name;
        }


        private bool IsAssignableToCore(UndefinedType self, IType type) => true;

        private bool IsAssignableToCore(VoidType self, IType type) => type is VoidType;
    }

}