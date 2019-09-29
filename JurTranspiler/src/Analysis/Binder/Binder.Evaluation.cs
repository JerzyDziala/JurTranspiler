using System;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.semantic_model;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.expressions;
using JurTranspiler.syntax_tree.Interfaces;
using UtilityLibrary;
using Type = System.Type;

namespace JurTranspiler.Analysis.Binder {

	public partial class Binder {

		public IType BindExpression(IExpressionSyntax syntax) {
			return symbols.AlreadyBound(syntax)
				       ? symbols.GetBindingFor(syntax)
				       : symbols.MakeBindingFor(syntax, BindExpressionCore((dynamic) syntax));
		}


		private FunctionPointerType BindExpressionCore(AnonymousFunctionSyntax syntax) {

			var returns = GetAllReturns(syntax);

			return returns switch {
				       var x when x.None() => CreateFunctionPointer(syntax, new VoidType()),
				       var x when x.AllHaveSame(y => y.Type) => CreateFunctionPointer(syntax, x[0].Type),
				       _ => AddErrorAndReturn(new UnableToInferReturnType(returns.GetLocations()), CreateFunctionPointer(syntax, new UndefinedType()))
				       };

		}


		private IType BindExpressionCore(NegationExpressionSyntax syntax) {
			var expressionType = BindExpression(syntax.Expression);

			if (!expressionType.IsBool()) {
				errors.Add(new NegationOperatorUsedWithNonBooleanType(syntax.File, syntax.Line, expressionType.Name));
			}

			return new PrimitiveType(PrimitiveKind.BOOL);
		}


		private IType BindExpressionCore(ArithmeticNegationExpressionSyntax syntax) {
			var expressionType = BindExpression(syntax.Expression);

			if (!expressionType.IsNum()) {
				errors.Add(new TypeMismatchInUseOfOperator(syntax.File,
				                                           syntax.Line,
				                                           "-",
				                                           expressionType.Name));
			}

			return new PrimitiveType(PrimitiveKind.NUM);
		}


		private IType BindExpressionCore(ArrayIndexAccessSyntax syntax) {
			var ownerType = BindExpression(syntax.Array);
			if (ownerType is ArrayType arrayType) return arrayType.ElementType;

			if (ownerType.IsNot<UndefinedType>())
				errors.Add(new IndexAccessOnNonArray(syntax.File, syntax.Line, ownerType.Name));

			return new UndefinedType();
		}


		private IType BindExpressionCore(GuardExpressionSyntax syntax) {

			var alwaysReturns = syntax.Guards.Any(x => x.IsOtherwisePattern);
			var onlyOneOtherwise = syntax.Guards.One(x => x.IsOtherwisePattern);

			if (!alwaysReturns)
				errors.Add(new NonExhaustingGuard(syntax.Location));

//			if(!onlyOneOtherwise)
			//TODO: warning - reduntant guard

			var types = syntax.Guards.Select(x => x.Expression).Select(BindExpression).ToImmutableArray();
			if (types.AllAreSame()) {
				return types.First();
			}
			return types.FirstOrDefault(type => types.All(x => IsAssignableTo(x, type))) ?? new AnyType();

		}


		private IType BindExpressionCore(IncrementationOrDecrementationExpression syntax) {
			if (syntax.Expression.IsNot<VariableAccessSyntax>()) {
				errors.Add(new InvalidUseOfOperator(syntax.Location, syntax.Operator));
				return new UndefinedType();
			}

			var type = BindExpression(syntax.Expression);

			switch (type) {
				case UndefinedType _:
					return type;
				case PrimitiveType primitiveType when primitiveType.PrimitiveKind == PrimitiveKind.NUM:
					return new PrimitiveType(PrimitiveKind.NUM);
				default:
					errors.Add(new TypeMismatchInUseOfOperator(syntax.Location, syntax.Operator, type.Name));
					return new UndefinedType();
			}
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
				       _ when syntax.IsNull => (IType) new AnyType(),
				       _ when syntax.IsUndefined => (IType) new AnyType(),
				       _ when syntax.Value.StartsWith("'") && syntax.Value.EndsWith("'") && syntax.Value.Length > 1 => new PrimitiveType(PrimitiveKind.STRING),
				       _ when double.TryParse(syntax.Value, out var _) => new PrimitiveType(PrimitiveKind.NUM),
				       _ when syntax.Value == "true" || syntax.Value == "false" => new PrimitiveType(PrimitiveKind.BOOL),
				       _ => throw new Exception("WTF")
				       };
		}


		private IType BindExpressionCore(VariableAccessSyntax syntax) {
			var declaration = syntax.GetVisibleDeclarationOrNull();

			if (declaration == null) {
				//error: use of undeclared variable
				errors.Add(new UseOfUndeclaredVariable(syntax.File, syntax.Line, syntax.Name));
				return new UndefinedType();
			}

			return BindVariableType(declaration);
		}

	}

}