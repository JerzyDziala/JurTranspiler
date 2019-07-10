using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.compilerSource.syntax_tree.searching;
using JurTranspiler.src.syntax_tree.types;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.Analysis {

	public static class Analyser {

		public static Binder Analyse(HashSet<Error> errors, SyntaxTree tree) {

			var binder = new Binder(tree);
			binder.BindAllTheThings(errors);

			var assignments = tree.AllChildren.OfType<IAssignment>();
			var functions = tree.AllChildren.OfType<FunctionDefinitionSyntax>().ToList();
			var forEachLoops = tree.AllChildren.OfType<ForEachStatementSyntax>();

			//multiple variables in scope
			foreach (var scope in functions.Concat<ISyntaxNode>(tree.AllChildren.OfType<MainSyntax>())) {
				var locals = scope.AllChildren.OfType<IVariableDeclarationSyntax>();
				var duplicates = locals.GroupBy(dec => dec.Name).Where(g => g.Count() > 1);
				foreach (var group in duplicates) {
					errors.Add(new MultipleDeclarationsOfVariableInScope(filesLinesLocations: group.Select(v => (v.File, v.Line)),
					                                                     name: group.First().Name));
				}
			}

			//mismatch in assignments
			foreach (var assignment in assignments) {
				var leftType = assignment.GetLeftType(errors, binder);
				var rightType = assignment.GetRightType(errors, binder);

				if (!rightType.IsAssignableTo(leftType, errors)) {
					errors.Add(new TypeMismatchInAssignmentError(file: assignment.File,
					                                             line: assignment.Line,
					                                             leftName: leftType.Name,
					                                             rightName: rightType.Name));
				}
			}

			//return type mismatch
			foreach (var function in functions.Where(x => !x.IsExtern)) {
				var signature = binder.BindFunctionDefinition(function, new HashSet<Error>());
				var returns = function.AllChildren.OfType<ReturnStatementSyntax>().ToList();

				if (!(signature.ReturnType is VoidType) && returns.None()) {
					errors.Add(new MissingReturnStatement(function.File, function.Line));
				}

				foreach (var returnStatement in returns) {
					var returnType = returnStatement.IsVoid
						                 ? new VoidType()
						                 : returnStatement.ReturnValue.Evaluate(new HashSet<Error>(), binder);

					if (!returnType.IsAssignableTo(signature.ReturnType, new HashSet<Error>())) {
						errors.Add(new TypeMismatchInReturnStatement(file: returnStatement.File,
						                                             line: returnStatement.Line,
						                                             returned: returnType.Name,
						                                             expected: signature.ReturnType.Name));
					}
				}

			}

			binder.GenerateNewCallableNames();
			return binder;
		}

	}

}