namespace JurTranspiler.compilerSource.semantic_model.functions {

	public interface ICallable {

		string Name { get; }
		int Arity { get; }
		IType ReturnType { get; }
		bool IsStatic { get; }
		string? StaticTypeName { get; }
		bool IsExtern { get; }
		bool IsMember { get; }

	}

}