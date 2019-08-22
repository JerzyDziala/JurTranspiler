using System;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {

        public IType BindExpression(IExpressionSyntax syntax) {
            return symbols.AlreadyBound(syntax)
                       ? symbols.GetBindingFor(syntax)
                       : symbols.MakeBindingFor(syntax, BindExpressionCore((dynamic) syntax));
        }


        private IType BindExpressionCore(AnonymousFunctionSyntax syntax) {

            Func<IType, IType> functionPointerTypeWithReturnType = returnType => new FunctionPointerType(returnType, syntax.Parameters.Select(x => BindType(x.Type!)));

            if (syntax.IsExpressionStatementLambda) {
                var bodyExpression = ((ExpressionStatementSyntax) syntax.Body).ExpressionSyntax;
                return functionPointerTypeWithReturnType(BindExpression(bodyExpression));
            }
            else {
                var block = (BlockStatement) syntax.Body;

                var returnStatements = block.Body.SelectMany(x => x.AllChildren)
                                            .OfType<ReturnStatementSyntax>()
                                            .ToImmutableArray();

                Func<IType> addUnableToInferAndReturnUndefined = () => {
                    errors.Add(new UnableToInferReturnType(returnStatements.GetLocations()));
                    return functionPointerTypeWithReturnType(new UndefinedType());
                };

                return returnStatements
                       .Select(x => x.IsVoid ? new VoidType() : BindExpression(x.ReturnValue!))
                       .ToImmutableArray() switch {
                    var x when x.None() => functionPointerTypeWithReturnType(new VoidType()),
                    var x when x.AllAreSame() => functionPointerTypeWithReturnType(x[0]),
                    _ => addUnableToInferAndReturnUndefined()
                };
            }

        }


        private IType BindExpressionCore(NegationExpressionSyntax syntax) {
            var expressionType = BindExpression(syntax.Expression);

            if (!expressionType.IsBool()) {
                errors.Add(new NegationOperatorUsedWithNonBooleanType(syntax.File, syntax.Line, expressionType.Name));
            }

            return new PrimitiveType(PrimitiveKind.BOOL);
        }


        private IType BindExpressionCore(ArrayIndexAccessSyntax syntax) {
            var ownerType = BindExpression(syntax.Array);
            if (ownerType is ArrayType arrayType) return arrayType.ElementType;

            errors.Add(new IndexAccessOnNonArray(syntax.File, syntax.Line));
            return new UndefinedType();
        }


        private IType BindExpressionCore(ConstructorSyntax syntax) => BindType(syntax.ConstructedType);


        private IType BindExpressionCore(FieldAccessSyntax syntax) {
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
                return matchingFields.First().Type;
            }
            if (!(ownerType is UndefinedType)) {
                errors.Add(new TriedToAccessFieldOnNonStruct(syntax.File,
                                                             syntax.Line,
                                                             syntax.Name,
                                                             ownerType.Name));
            }
            return new UndefinedType();
        }


        private IType BindExpressionCore(FunctionCallSyntax syntax) => BindFunctionCall(syntax).Callable.ReturnType;

        private IType BindExpressionCore(DefaultTypeValueSyntax syntax) => BindType(syntax.Type);


        private IType BindExpressionCore(TypeExpressionSyntax syntax) {
            var type = BindType(syntax.Type);
            Func<string, IType> findType = name => symbols.TypesBindings.Values
                                                          .Concat(symbols.OpenStructsBinding.Values)
                                                          .FirstOrDefault(x => x.Name == name);
            var typeType = type switch {
                FunctionPointerType _ => findType(nameof(FunctionPointerType)),
                UndeclaredStructType _ => findType(nameof(Type)),
                StructType _ => findType(nameof(StructType)),
                ArrayType _ => findType(nameof(ArrayType)),
                PrimitiveType _ => findType(nameof(PrimitiveType)),
                var x when x is TypeParameterType || x is UndefinedType || x is AnyType => findType(nameof(Type)),
                _ => throw new Exception("Invalid Type Expression Type")
            };
            if (typeType == null) {
                throw new Exception("You forgot to add the runtime library");
            }
            return typeType;
        }


        private IType BindExpressionCore(OperationSyntax syntax) {
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


        private IType BindExpressionCore(ParenthesisSyntax syntax) => BindExpression(syntax.Expression);


        private IType BindExpressionCore(PrimitiveValueSyntax syntax) {
            return syntax switch {
                _ when syntax.IsNull => (IType) new NullType(),
                _ when syntax.Value.StartsWith("'") && syntax.Value.EndsWith("'") && syntax.Value.Length > 1 => new PrimitiveType(PrimitiveKind.STRING),
                _ when double.TryParse(syntax.Value, out var _) => new PrimitiveType(PrimitiveKind.NUM),
                _ when syntax.Value == "true" || syntax.Value == "false" => new PrimitiveType(PrimitiveKind.BOOL),
                _ => throw new Exception("WTF")
            };
        }


        private IType BindExpressionCore(VariableAccessSyntax syntax) {
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