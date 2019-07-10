using System.Collections.Immutable;

namespace JurTranspiler.compilerSource.semantic_model.functions {

	public class FunctionPointer : Callable {

		public override string Name { get; }
		public override ImmutableList<Type> ParametersTypes{ get; }
		public override ImmutableList<Type> TypeParameters { get; }
		public override int GenericArity { get; }
        public override int Arity { get; }
		public override Type ReturnType { get; }


		public FunctionPointer(string name, ImmutableList<Type> parametersTypes, Type returnType) {
			Name = name;
			ParametersTypes= parametersTypes;
			ReturnType = returnType;
            Arity = ParametersTypes.Count;
			GenericArity = 0;
			TypeParameters = ImmutableList<Type>.Empty;
		}

	}

}