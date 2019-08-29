using System;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {

        public IType BindVariableType(IVariableDeclarationSyntax variable) {
            return variable is InferredVariableDeclarationSyntax inferred
                       ? BindInferredVariable(inferred)
                       : BindType(variable.Type!);
        }


        private IType BindInferredVariable(InferredVariableDeclarationSyntax syntax) {
            var type = BindExpression(syntax.Initializer);
            if (type is VoidType) {
                errors.Add(new TypeMismatchInAssignmentError(syntax.File, syntax.Line, "Undefined", type.Name));
                return new UndefinedType();
            }
            return type;
        }

    }

}