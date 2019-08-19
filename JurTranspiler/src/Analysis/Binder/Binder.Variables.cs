using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.Analysis {

	public partial class Binder {

		public IType BindVariableType(IVariableDeclarationSyntax variable) {
			return variable is InferredVariableDeclarationSyntax inferred
				       ? BindExpression(inferred.Initializer)
				       : BindType(variable.Type!);
		}

	}

}