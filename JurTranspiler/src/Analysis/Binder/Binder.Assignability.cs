using System.Collections.Generic;
using System.Linq;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {
        public bool IsAssignableTo(Type a, Type b) {
            return symbols.AlreadyBound((a, b))
                       ? symbols.GetBindingFor((a, b))
                       : symbols.MakeBindingFor((a, b), IsAssignableToCore((dynamic) a, b));
        }


        private bool IsAssignableToCore(ArrayType self, Type type) {
            if (type is AnyType) return true;
            if (type is UndefinedType) return true;
            if (type is ArrayType arrayType && IsAssignableTo(self.ElementType, arrayType.ElementType)) return true;
            return false;
        }


        private bool IsAssignableToCore(FunctionPointerType self, Type type) {
            if (type is AnyType) return true;
            if (type is UndefinedType) return true;
            if (type is FunctionPointerType func && IsAssignableTo(self.ReturnType, func.ReturnType)) {
                if (self.Parameters.Count != func.Parameters.Count) return false;
                return func.Parameters.Where((t, i) => {
                    var isNot = !IsAssignableTo(t, self.Parameters[i]);
                    return isNot;
                }).None();
            }
            return false;
        }


        private bool IsAssignableToCore(NullType self, Type type) => true;


        private bool IsAssignableToCore(PrimitiveType self, Type type) {
            if (type is AnyType) return true;
            if (type is UndefinedType) return true;
            if (type is PrimitiveType p) {
                if (self.PrimitiveKind == p.PrimitiveKind) return true;
            }
            return false;
        }


        private bool IsAssignableToCore(AnyType self, Type type) => type is AnyType || type is UndefinedType;


        private bool IsAssignableToCore(StructType self, Type type) {
            if (type is AnyType) return true;
            if (type is UndefinedType) return true;
            if (type is StructType target) {
                var fieldsA = BindFields(self);
                var fieldsB = BindFields(target);
                return fieldsB.All(fieldB => fieldsA.Any(fieldA => fieldA.Name == fieldB.Name
                                                                && fieldA.Type.Equals(fieldB.Type)));
            }
            return false;
        }


        private bool IsAssignableToCore(TypeParameterType self, Type type) {
            if (type is AnyType) return true;
            if (type is UndefinedType) return true;
            if (type is TypeParameterType t1) {
                if (t1.Name == self.Name) return true;
                if (self.GetAllConstraints().Any(c => c.Equals(t1))) return true;
            }
            return false;
        }


        private bool IsAssignableToCore(UndeclaredStructType self, Type type) {
            if (type is AnyType) return true;
            if (type is UndeclaredStructType undeclaredType
             && undeclaredType.Name == self.Name
             || type is UndefinedType) return true;
            return false;
        }


        private bool IsAssignableToCore(UndefinedType self, Type type) => true;

        private bool IsAssignableToCore(VoidType self, Type type) => type is VoidType || type is UndefinedType;
    }

}