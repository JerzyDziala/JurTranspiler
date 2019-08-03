using System;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {

        public Type BindExpression(IExpressionSyntax syntax) {
            return symbols.AlreadyBound(syntax)
                       ? symbols.GetBindingFor(syntax)
                       : symbols.MakeBindingFor(syntax, BindExpressionCore((dynamic) syntax));
        }


        private Type BindExpressionCore(AnonymousFunctionSyntax syntax) {

            Func<Type, Type> functionPointerTypeWithReturnType = returnType => new FunctionPointerType(returnType, syntax.Parameters.Select(x => BindType(x.Type)));

            if (syntax.IsExpressionStatementLambda) {
                var bodyExpression = ((ExpressionStatementSyntax) syntax.Body).ExpressionSyntax;
                return functionPointerTypeWithReturnType(BindExpression(bodyExpression));
            }
            else {
                var block = ((BlockStatement) syntax.Body);

                var returnStatements = block.Body.SelectMany(x => x.AllChildren)
                                            .OfType<ReturnStatementSyntax>()
                                            .ToImmutableArray();

                Func<Type> addUnableToInferAndReturnUndefined = () => {
                    errors.Add(new UnableToInferReturnType(returnStatements.GetLocations()));
                    return functionPointerTypeWithReturnType(new UndefinedType());
                };

                return returnStatements
                       .Select(x => x.IsVoid ? new VoidType() : BindExpression(x.ReturnValue))
                       .ToImmutableArray() switch {
                           var x when x.None() => functionPointerTypeWithReturnType(new VoidType()),
                           var x when x.AllAreSame() => functionPointerTypeWithReturnType(x[0]),
                           _ => addUnableToInferAndReturnUndefined()
                           };
            }

        }


        private Type BindExpressionCore(ArrayIndexAccessSyntax syntax) {
            var ownerType = BindExpression(syntax.Array);
            if (ownerType is ArrayType arrayType) return arrayType.ElementType;

            errors.Add(new IndexAccessOnNonArray(syntax.File, syntax.Line));
            return new UndefinedType();
        }


        private Type BindExpressionCore(ConstructorSyntax syntax) => BindType(syntax.ConstructedType);


        private Type BindExpressionCore(FieldAccessSyntax syntax) {
            var ownerType = BindExpression(syntax.Owner);
            if (ownerType is StructType structType) {
                var fields = BindFields(structType);
                var matchingFields = fields.Where(field => field.Name == syntax.Name).ToList();
                if (matchingFields.None()) {
                    //error: no matching field
                    errors.Add(new NoMatchingFieldFound(file: syntax.File,
                                                        line: syntax.Line,
                                                        fieldName: syntax.Name,
                                                        typeName: ownerType.Name));
                    return new UndefinedType();
                }
                if (matchingFields.Count > 1) {
                    //error: ambiguous field reference
                    errors.Add(new AmbiguousFieldReference(file: syntax.File,
                                                           line: syntax.Line,
                                                           fieldName: syntax.Name,
                                                           typeName: ownerType.Name));
                    return new UndefinedType();
                }
                else return matchingFields.First().Type;
            }
            return new UndefinedType();
        }


        private Type BindExpressionCore(FunctionCallSyntax syntax) => BindFunctionCall(syntax).Callable.ReturnType;

        private Type BindExpressionCore(DefaultTypeValueSyntax syntax) => BindType(syntax.Type);


        private Type BindExpressionCore(OperationSyntax syntax) {
            var left = BindExpression(syntax.Left);
            var right = BindExpression(syntax.Right);
            if (syntax.Operator == "+") {
                if (left.Name == "num" && right.Name == "num") {
                    return new PrimitiveType(PrimitiveKind.NUM);
                }
                if (left.Name == "num" && right.Name == "string") {
                    return new PrimitiveType(PrimitiveKind.STRING);
                }
                if (left.Name == "string" && right.Name == "num") {
                    return new PrimitiveType(PrimitiveKind.STRING);
                }
                if (left.Name == "string" && right.Name == "string") {
                    return new PrimitiveType(PrimitiveKind.STRING);
                }
                if (left.Name == "string" && right.Name == "bool") {
                    return new PrimitiveType(PrimitiveKind.STRING);
                }
                if (left.Name == "bool" && right.Name == "string") {
                    return new PrimitiveType(PrimitiveKind.STRING);
                }
                if (!(left is UndefinedType) && !(right is UndefinedType)) {
                    errors.Add(new TypeMismatchInUseOfOperator(file: syntax.File,
                                                               line: syntax.Line,
                                                               op: syntax.Operator,
                                                               leftName: left.Name,
                                                               rightName: right.Name));
                }
                return new UndefinedType();
            }
            else if (syntax.Operator == "-" || syntax.Operator == "*" || syntax.Operator == "/") {
                if (left.Name == "num" && right.Name == "num") {
                    return new PrimitiveType(PrimitiveKind.NUM);
                }
                if (!(left is UndefinedType) && !(right is UndefinedType)) {
                    errors.Add(new TypeMismatchInUseOfOperator(file: syntax.File,
                                                               line: syntax.Line,
                                                               op: syntax.Operator,
                                                               leftName: left.Name,
                                                               rightName: right.Name));
                }
                return new UndefinedType();
            }
            else if (syntax.Operator == ">" || syntax.Operator == "<" || syntax.Operator == ">=" || syntax.Operator == "<=") {
                if (left.Name == "num" && right.Name == "num") {
                    return new PrimitiveType(PrimitiveKind.BOOL);
                }
                if (!(left is UndefinedType) && !(right is UndefinedType)) {
                    errors.Add(new TypeMismatchInUseOfOperator(file: syntax.File,
                                                               line: syntax.Line,
                                                               op: syntax.Operator,
                                                               leftName: left.Name,
                                                               rightName: right.Name));
                }
                return new UndefinedType();
            }
            else if (syntax.Operator == "==" || syntax.Operator == "!=" || syntax.Operator == "is") {
                if (IsAssignableTo(left, right) || IsAssignableTo(right, left)) {
                    return new PrimitiveType(PrimitiveKind.BOOL);
                }
                if (!(left is UndefinedType) && !(right is UndefinedType)) {
                    errors.Add(new TypeMismatchInUseOfOperator(file: syntax.File,
                                                               line: syntax.Line,
                                                               op: syntax.Operator,
                                                               leftName: left.Name,
                                                               rightName: right.Name));
                }
                return new UndefinedType();
            }
            else if (syntax.Operator == "&&" || syntax.Operator == "||" || syntax.Operator == "and" || syntax.Operator == "or") {
                if (left.Name == "bool" && right.Name == "bool") {
                    return new PrimitiveType(PrimitiveKind.BOOL);
                }
                if (!(left is UndefinedType) && !(right is UndefinedType)) {
                    errors.Add(new TypeMismatchInUseOfOperator(file: syntax.File,
                                                               line: syntax.Line,
                                                               op: syntax.Operator,
                                                               leftName: left.Name,
                                                               rightName: right.Name));
                }
                return new UndefinedType();
            }

            throw new Exception("WTF! invalid operator assigned during parsing?");

        }


        private Type BindExpressionCore(ParenthesisSyntax syntax) => BindExpression(syntax.Expression);


        private Type BindExpressionCore(PrimitiveValueSyntax syntax) {
            if (syntax.IsNull) return new NullType();
            if (syntax.Value.StartsWith("'") && syntax.Value.EndsWith("'") && syntax.Value.Length > 1) {
                return new PrimitiveType(PrimitiveKind.STRING);
            }
            if (double.TryParse(syntax.Value, out double _)) {
                return new PrimitiveType(PrimitiveKind.NUM);
            }
            if (syntax.Value == "true" || syntax.Value == "false") {
                return new PrimitiveType(PrimitiveKind.BOOL);
            }
            else throw new Exception("WTF");
        }


        private Type BindExpressionCore(VariableAccessSyntax syntax) {
            var declaration = symbols.GetVisibleDeclarationOrNull(syntax);
            if (declaration == null) {
                //error: use of undeclared variable
                errors.Add(new UseOfUndeclaredVariable(syntax.File, syntax.Line, syntax.Name));
                return new UndefinedType();
            }
            return BindVariableType(declaration);
        }
    }

}