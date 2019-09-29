using System.Collections.Immutable;
using JurTranspiler.semantic_model.functions;

namespace JurTranspiler.semantic_model {

    public class FunctionCallInfo {
        public ICallable Callable { get; }
        public ImmutableHashSet<Substitution> Substitutions { get; }


        public FunctionCallInfo(ICallable callable, ImmutableHashSet<Substitution> substitutions) {
            Callable = callable;
            Substitutions = substitutions;
        }
    }

}