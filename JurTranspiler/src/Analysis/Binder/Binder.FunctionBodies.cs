using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Antlr4.Runtime.Atn;
using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.compilerSource.semantic_model.functions;
using JurTranspiler.src.syntax_tree.types;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.Analysis {

	public partial class Binder {

		public void BindFunctionBodies() {

			var functions = symbols.Tree.AllFunctionDefinitions;
			var lambdas = symbols.Tree.AllLambdas;
			var mains = symbols.Tree.AllMains;

			CheckForInvalidAssignments();
			CheckForInvalidInitializers();

			foreach (var function in functions) {

				if (!function.IsExtern) {
					CheckForDuplicateVariables(function);
					CheckForNonReturningCodePaths(function);
					CheckForReturnTypeMismatch(function);
				}

			}

			foreach (var lambda in lambdas) {
				CheckForNonReturningCodePaths(lambda);
			}

			foreach (var main in mains) {
				CheckForDuplicateVariables(main);
			}
		}


		private void CheckForInvalidInitializers() {

			var constructors = symbols.Tree.AllConstructors;

			foreach (var constructor in constructors) {

				var initializedType = BindType(constructor.ConstructedType) as StructType;

				if (initializedType is null)
					continue;

				foreach (var initializer in constructor.Initializers) {
					CheckInitializerValidity(initializer, initializedType);
				}
			}

		}


		private void CheckInitializerValidity(InitializerSyntax initializerSyntax, StructType type) {
			var assignedField = BindFields(type).FirstOrDefault(x => x.Name == initializerSyntax.FieldName);
			if (assignedField == null) {
				errors.Add(new NoMatchingFieldFound(initializerSyntax.Location,
				                                    initializerSyntax.FieldName,
				                                    type.Name));
				return;
			}
			var initializerExpressionType = BindExpression(initializerSyntax.Expression);
			if (!IsAssignableTo(initializerExpressionType, assignedField.Type)) {
				errors.Add(new TypeMismatchInAssignmentError(initializerSyntax.Location,
				                                             assignedField.Type.Name,
				                                             initializerExpressionType.Name));
			}
		}


		private void CheckForDuplicateVariables(ITreeNode scope) {

			var localsAndParameters = scope.AllChildren
			                               .OfType<IVariableDeclarationSyntax>()
			                               .ToImmutableList();

			var duplicatesGroup = localsAndParameters.Select(x => x.GetVisibleVariablesInScope()
			                                                       .Where(v => v.Name == x.Name)
			                                                       .Concat(new[] { x })
			                                                       .ToImmutableList())
			                                         .Where(x => x.MoreThenOne())
			                                         .ToImmutableList();

			Predicate<IEnumerable<IVariableDeclarationSyntax>> isSubsetOfOther = enumerable => duplicatesGroup.Any(enumerable.IsSuperSetOf);

			var withoutSubSets = duplicatesGroup.RemoveAll(isSubsetOfOther);

			foreach (var group in withoutSubSets) {
				errors.Add(new MultipleDeclarationsOfVariableInScope(locations: group.Select(v => new Location(v.File, v.Line)),
				                                                     name: group.First().Name));
			}
		}


		private void CheckForNonReturningCodePaths(IFunctionDefinitionOrLambdaSyntax syntax) {

			if (syntax.IsArrow) return;
			if (syntax is FunctionDefinitionSyntax function && BindFunctionDefinition(function).ReturnType is VoidType) return;
			if (syntax is AnonymousFunctionSyntax lambda && BindExpression(lambda).As<FunctionPointerType>()!.ReturnType is VoidType) return;

			var body = syntax.Body?.As<BlockStatement>();

			if (!AllPathsReturnValue(body?.Body ?? ImmutableArray<IStatementSyntax>.Empty)) {
				errors.Add(new NotAllCodePathReturnValue(syntax));
			}
		}


		private bool AllPathsReturnValue(ImmutableArray<IStatementSyntax> nodes) {

			if (nodes.None()) return false;

			var onlyControlFlow = nodes.FlattenBlockStatements()
			                           .Where(x => x is IfStatementSyntax || x is ReturnStatementSyntax)
			                           .ToImmutableArray();

			if (onlyControlFlow.Any(x => x is ReturnStatementSyntax)) return true;

			return onlyControlFlow.OfType<IfStatementSyntax>().Any(x => {

				var body = x.Body.As<GeneratedScopeSyntax>()!.Body;
				var elseBody = x.ElseBody?.As<GeneratedScopeSyntax>()?.Body;

				var ifBodyReturns = AllPathsReturnValue(body.AsImmutableArray());
				var elseBodyReturns = AllPathsReturnValue(elseBody?.AsImmutableArray() ?? ImmutableArray<IStatementSyntax>.Empty);

				return ifBodyReturns && elseBodyReturns;
			});
		}


		private void CheckForInvalidAssignments() {
			var assignments = symbols.Tree.AllAssignments;
			foreach (var assignment in assignments) {
				var (leftType, rightType) = BindAssignment(assignment);

				if (!IsAssignableTo(rightType, leftType)) {
					errors.Add(new TypeMismatchInAssignmentError(file: assignment.File,
					                                             line: assignment.Line,
					                                             leftName: leftType.Name,
					                                             rightName: rightType.Name));
				}
			}
		}


		private void CheckForReturnTypeMismatch(FunctionDefinitionSyntax function) {

			if (function.IsExtern) return;

			var signature = BindFunctionDefinition(function);
			var returns = GetAllReturns(function);

			bool returnedTypeIsNotAssignableToDeclaredReturnType(FunctionReturn returned) => !IsAssignableTo(returned.Type, signature.ReturnType);

			returns.Where(returnedTypeIsNotAssignableToDeclaredReturnType)
			       .ForEach(x => errors.Add(new TypeMismatchInReturnStatement(x.Location, x.Type.Name, signature.ReturnType.Name)));

		}

	}

}