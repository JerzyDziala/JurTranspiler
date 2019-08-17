using System.Collections.Immutable;

namespace JurTranspiler.compilerSource.semantic_model.functions {

    public class ErrorSignature : Callable {

        public ErrorSignature(IType returnType) : base("", ImmutableArray<IType>.Empty, ImmutableArray<IType>.Empty, returnType) {
        }
    }

}