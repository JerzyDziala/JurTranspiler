using System.Collections.Immutable;
using System.Linq;

namespace JurTranspiler.compilerSource.semantic_model.functions {

	public class Dispatcher : ICallable {

		public string Name { get; }
		public int Arity { get; }
		public IType ReturnType { get; }
		public bool IsStatic => false;
		public string? StaticTypeName => null;
		public bool IsExtern => false;
		public bool IsMember => false;
		public ImmutableArray<FunctionCallInfo> Functions;


		public Dispatcher(ImmutableArray<FunctionCallInfo> functions) {
			Functions = functions;
			Name = functions.First().Callable.Name;
			Arity = functions.First().Callable.Arity;
			ReturnType = functions.First().Callable.ReturnType;
		}

	}

}