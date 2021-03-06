using System;
using JurTranspiler.semantic_model.types;

namespace JurTranspiler.semantic_model {

    public class Substitution : IEquatable<Substitution> {

        public TypeParameterType typeParameter { get; }
        public IType typeArgument { get; }


        public Substitution(TypeParameterType typeParameter, IType typeArgument) {
            this.typeParameter = typeParameter;
            this.typeArgument = typeArgument;
        }


        public bool Equals(Substitution other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(typeParameter, other.typeParameter) && Equals(typeArgument, other.typeArgument);
        }


        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj?.GetType() != GetType()) return false;
            return Equals((Substitution) obj!);
        }


        public override int GetHashCode() {
            unchecked {
                return ((typeParameter?.GetHashCode() ?? 0) * 397) ^ (typeArgument?.GetHashCode() ?? 0);
            }
        }
    }

}