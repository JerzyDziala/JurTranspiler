using System.Collections.Immutable;
using JurTranspiler.semantic_model.types;

namespace JurTranspiler.semantic_model.functions {

    public class ErrorSignature : Callable {

        public ErrorSignature(IType returnType) : base("", ImmutableArray<IType>.Empty, ImmutableArray<IType>.Empty, returnType) {
        }
    }

}