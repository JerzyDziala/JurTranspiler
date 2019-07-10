using System.Collections.Immutable;
using System.Linq;

namespace JurTranspiler.compilerSource.semantic_model.functions {

    public class Dispatcher : ICallable{

        public string Name { get; }
        public int Arity { get; }
        public Type ReturnType { get; }
        public ImmutableList<Callable> Functions;


        public Dispatcher(ImmutableList<Callable> functions) {
            Functions = functions;
            Name = functions.First().Name;
            Arity = functions.First().Arity;
            ReturnType = functions.First().ReturnType;
        }
    }

}