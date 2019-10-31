using System;
using System.Collections.Generic;
using System.Linq;
using JurTranspiler.semantic_model;
using JurTranspiler.semantic_model.types;
using UtilityLibrary;

namespace JurTranspiler.Analysis.Binder {
    public partial class Binder {
        
        private bool InheritsFrom(StructType a, StructType b) {
            if (Equals(a, b)) {
                return true;
            }
            if (a.NonGenericName == b.NonGenericName && a.Arity == b.Arity) {
                return a.TypeArguments.All((typeArgument,i) => IsAssignableTo(typeArgument.Value, b.TypeArguments[i].Value));
            }
            return a.InlinedTypes.Any(aInline => aInline.Value is StructType aParentType && InheritsFrom(aParentType, b));
        }

        private bool InheritsFromWithSubstitutions(StructType a, IType b, ISet<Substitution> substitutions) {

            if (b is TypeParameterType typeParameter) {
                substitutions.Add(new Substitution(typeParameter, a));
                return true;
            }

            if (b is AnyType || b is UndefinedType) {
                return true;
            }

            if (b is UndeclaredStructType undefinedStruct) {
                return a.Name == undefinedStruct.Name;
            }

            if (b is StructType structType) {
                if (structType.NonGenericName == a.NonGenericName && structType.Arity == a.Arity) {
                    return a.TypeArguments.All((typeArgument,i) => IsAssignableToWithSubstitutions(typeArgument.Value, structType.TypeArguments[i].Value, substitutions));
                }
                return a.InlinedTypes.Any(aInline => aInline.Value is StructType aParentType && InheritsFromWithSubstitutions(aParentType, b,substitutions));
            }

            return false;
        }
    }
}