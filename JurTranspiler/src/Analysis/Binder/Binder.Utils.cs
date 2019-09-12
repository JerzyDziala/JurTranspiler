using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.compilerSource.Analysis {

	public partial class Binder {

		private ImmutableArray<FunctionReturn> GetAllReturns(IFunctionDefinitionOrLambdaSyntax function) {
			if (function.IsArrow) {
				var returnExpression = ((ExpressionStatementSyntax) function.Body!).ExpressionSyntax;
				var toReturn = new FunctionReturn(returnExpression.Location, BindExpression(returnExpression));
				return ImmutableArray.Create(toReturn);
			}
			return function.TopLevelReturns.Select(InferReturnedType).ToImmutableArray();
		}


		private FunctionReturn InferReturnedType(ReturnStatementSyntax syntax) {
			return syntax.IsVoid
				       ? new FunctionReturn(syntax.Location, new VoidType())
				       : new FunctionReturn(syntax.Location, BindExpression(syntax.ReturnValue!));
		}


		private FunctionPointerType CreateFunctionPointer(IFunctionDefinitionOrLambdaSyntax syntax, IType returnType) {
			return new FunctionPointerType(returnType, syntax.Parameters.Select(x => BindType(x.Type!)));
		}


		private R AddErrorAndReturn<R>(Error error, R toReturn) {
			errors.Add(error);
			return toReturn;
		}

	}


	public static class BindingExtensions {

		public static ImmutableArray<Location> GetLocations(this IEnumerable<IHaveLocation> things) {
			return things.Select(x => x.Location).ToImmutableArray();
		}


		public static ImmutableArray<IType> GetTypes(this IEnumerable<IHaveType> things) {
			return things.Select(x => x.Type).ToImmutableArray();
		}


		public static ImmutableList<IStatementSyntax> FlattenBlockStatements(this IEnumerable<IStatementSyntax> statements) {
			return statements.Aggregate(ImmutableList<IStatementSyntax>.Empty,
			                            (list, syntax) => syntax is BlockStatement block
				                                              ? list.AddRange(block.Body)
				                                              : list.Add(syntax));

		}

	}

}