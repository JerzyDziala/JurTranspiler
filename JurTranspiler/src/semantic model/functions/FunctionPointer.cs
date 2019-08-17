using System.Collections.Immutable;

namespace JurTranspiler.compilerSource.semantic_model.functions {

	public class FunctionPointer : Callable {

		public FunctionPointer(string name, ImmutableArray<IType> parametersTypes, IType returnType) : base(name, parametersTypes,ImmutableArray<IType>.Empty, returnType){

		}

	}

}