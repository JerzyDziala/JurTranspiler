using System;
using System.Reflection.Metadata.Ecma335;

namespace JurTranspiler.compilerSource.semantic_model {

    public static class TypesExtensions {
        public static bool IsBool(this IType type) => type is PrimitiveType primitiveType && primitiveType.PrimitiveKind == PrimitiveKind.BOOL;


        public static bool IsBool(this IType type, out PrimitiveType? casted) {

            if (type is PrimitiveType primitiveType && primitiveType.PrimitiveKind == PrimitiveKind.BOOL) {
                casted = primitiveType;
                return true;
            }

            casted = null;
            return false;
        }
    }

}