using System.Collections.Generic;
using JurTranspiler.compilerSource.nodes;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.Analysis {

	public partial class Binder {

		public Type BindVariableType(IVariableDeclarationSyntax variable, HashSet<Error> errors) {
			if (variable is InferredVariableDeclarationSyntax inferred)
				return inferred.Initializer.Evaluate(errors, this);

			return BindType(variable.Type,errors);
		}

	}

}