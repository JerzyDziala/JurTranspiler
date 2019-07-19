using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.src.syntax_tree.types;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.Analysis {

    public static class Analyser {

        public static Knowledge Analyse(HashSet<Error> errors, SyntaxTree tree) {

            var binder = new Binder(tree, errors);


            //multiple variables in scope
            foreach (var scope in tree.AllFunctions.Concat<ISyntaxNode>(tree.AllMains)) {
                var locals = scope.AllChildren.OfType<IVariableDeclarationSyntax>();
                var duplicates = locals.GroupBy(dec => dec.Name).Where(g => g.Count() > 1);
                foreach (var group in duplicates) {
                    errors.Add(new MultipleDeclarationsOfVariableInScope(filesLinesLocations: group.Select(v => (v.File, v.Line)),
                                                                         name: group.First().Name));
                }
            }

            //mismatch in assignments
            foreach (var assignment in tree.AllAssignments) {
                var (leftType, rightType) = binder.BindAssignment(assignment);

                if (!binder.IsAssignableTo(rightType, leftType)) {
                    errors.Add(new TypeMismatchInAssignmentError(file: assignment.File,
                                                                 line: assignment.Line,
                                                                 leftName: leftType.Name,
                                                                 rightName: rightType.Name));
                }
            }

            //return type mismatch
            foreach (var function in tree.AllFunctions.Where(x => !x.IsExtern)) {
                var signature = binder.BindFunctionDefinition(function);
                var returns = function.AllChildren.OfType<ReturnStatementSyntax>().ToList();

                if (!(signature.ReturnType is VoidType) && returns.None()) {
                    errors.Add(new MissingReturnStatement(function.File, function.Line));
                }

                foreach (var returnStatement in returns) {
                    var returnType = returnStatement.IsVoid
                                         ? new VoidType()
                                         : binder.BindExpression(returnStatement.ReturnValue);

                    if (!binder.IsAssignableTo(returnType, signature.ReturnType)) {
                        errors.Add(new TypeMismatchInReturnStatement(file: returnStatement.File,
                                                                     line: returnStatement.Line,
                                                                     returned: returnType.Name,
                                                                     expected: signature.ReturnType.Name));
                    }
                }

            }

            binder.GenerateNewCallableNames();
            return binder.Knowledge;
        }

    }

}