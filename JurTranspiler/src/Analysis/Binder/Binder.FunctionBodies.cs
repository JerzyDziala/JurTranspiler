using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.Analysis.errors.bases;
using JurTranspiler.semantic_model;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.declarations;
using JurTranspiler.syntax_tree.expressions;
using JurTranspiler.syntax_tree.Interfaces;
using JurTranspiler.syntax_tree.statements;
using UtilityLibrary;

namespace JurTranspiler.Analysis.Binder {

	public partial class Binder {

		public void BindFunctionBodies() {

			var functions = symbols.Tree.AllFunctionDefinitions;
			var lambdas = symbols.Tree.AllLambdas;
			var mains = symbols.Tree.AllMains;

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


			var onlyControlFlow = flattenBlockStatements(nodes)
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

			static ImmutableList<IStatementSyntax> flattenBlockStatements(IEnumerable<IStatementSyntax> statements) {
				return statements.Aggregate(ImmutableList<IStatementSyntax>.Empty,
											(list, syntax) => syntax is BlockStatement block
																  ? list.AddRange(block.Body)
																  : list.Add(syntax));
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