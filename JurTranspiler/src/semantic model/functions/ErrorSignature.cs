using System.Collections.Immutable;

namespace JurTranspiler.compilerSource.semantic_model.functions {

    public class ErrorSignature : Callable{

        public override string Name { get; }
        public override ImmutableList<Type> ParametersTypes { get; }
        public override ImmutableList<Type> TypeParameters { get; }
        public override int GenericArity { get; }
        public override int Arity { get; }
        public override Type ReturnType { get; }
        public int Abstraction { get; }


        public ErrorSignature(Type returnType) {
            Name = "";
            ParametersTypes = ImmutableList<Type>.Empty;
            TypeParameters = ImmutableList<Type>.Empty;
            ReturnType = returnType;
            Abstraction = 0;
            GenericArity = 0;
            Arity = 0;
        }
    }

}