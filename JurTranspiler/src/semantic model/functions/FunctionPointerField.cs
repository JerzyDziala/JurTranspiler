using System.Collections.Immutable;
using JurTranspiler.semantic_model.types;

namespace JurTranspiler.semantic_model.functions {

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