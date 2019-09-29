using JurTranspiler.Analysis.errors;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.declarations;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.Analysis.Binder {

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