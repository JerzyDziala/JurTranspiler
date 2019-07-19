using System.Collections.Generic;
using JurTranspiler.compilerSource.nodes;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {

        public Type BindVariableType(IVariableDeclarationSyntax variable) {
            return variable is InferredVariableDeclarationSyntax inferred
                       ? BindExpression(inferred.Initializer)
                       : BindType(variable.Type);
        }

    }

}