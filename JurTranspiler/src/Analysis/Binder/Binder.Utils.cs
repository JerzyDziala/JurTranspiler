using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis.errors.bases;
using JurTranspiler.semantic_model;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.Interfaces;
using JurTranspiler.syntax_tree.statements;

namespace JurTranspiler.Analysis.Binder {

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



}