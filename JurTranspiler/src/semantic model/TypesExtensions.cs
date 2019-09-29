using JurTranspiler.semantic_model.types;

namespace JurTranspiler.semantic_model {

	public static class TypesExtensions {

		public static bool IsBool(this IType type) => type is PrimitiveType primitiveType && primitiveType.PrimitiveKind == PrimitiveKind.BOOL;
		public static bool IsNum(this IType type) => type is PrimitiveType primitiveType && primitiveType.PrimitiveKind == PrimitiveKind.NUM;

	}

}