using System.Collections.Immutable;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;

namespace JurTranspiler.compilerSource.semantic_model.functions {

	public class FunctionPointerField : Callable {

		public override bool IsMember => true;
		public IType OwnerType { get; }


		public FunctionPointerField(IType ownerType,
		                            string name,
		                            FunctionPointerType type) : base(name,
		                                                             type.Parameters,
		                                                             ImmutableArray<IType>.Empty,
		                                                             type.ReturnType) {
			OwnerType = ownerType;

		}

	}

}