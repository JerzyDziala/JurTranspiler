using System.Collections.Immutable;
using System.Linq;

namespace JurTranspiler.compilerSource.semantic_model.functions {

    public class Dispatcher : ICallable {

        public string Name { get; }
        public int Arity { get; }
        public Type ReturnType { get; }
        public ImmutableList<FunctionCallInfo> Functions;


        public Dispatcher(ImmutableList<FunctionCallInfo> functions) {
            Functions = functions;
            Name = functions.First().Callable.Name;
            Arity = functions.First().Callable.Arity;
            ReturnType = functions.First().Callable.ReturnType;
        }
    }

}