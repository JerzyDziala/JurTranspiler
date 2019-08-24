using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Antlr4.Runtime.Atn;
using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {

        public void BindFunctionBodies() {
            var functionsAndMains = symbols.Tree.AllFunctionDefinitions.Concat<ISyntaxNode>(symbols.Tree.AllMains);
            foreach (var body in functionsAndMains) {
                BindFunctionBody(body);
            }
        }


        private void BindFunctionBody(ISyntaxNode scope) {
            CheckForInvalidAssignments(scope);
            CheckForDuplicateVariables(scope);

            //TODO: check for returns in main
            if (scope is FunctionDefinitionSyntax notMain) CheckForReturnTypeMismatch(notMain);
        }


        private void CheckForDuplicateVariables(ISyntaxNode scope) {

            var localsAndParameters = scope.AllChildren
                                           .OfType<IVariableDeclarationSyntax>()
                                           .ToImmutableList();

            var duplicatesGroup = localsAndParameters.Select(x => x.GetVisibleVariablesInScope()
                                                                   .Where(v => v.Name == x.Name)
                                                                   .Concat(new[] {x}))
                                                     .Where(x => x.MoreThenOne())
                                                     .ToImmutableList();

            Predicate<IEnumerable<IVariableDeclarationSyntax>> isSubsetOfOther = enumerable => duplicatesGroup.Any(enumerable.IsSuperSetOf);

            var withoutSubSets = duplicatesGroup.RemoveAll(isSubsetOfOther);

            foreach (var group in withoutSubSets) {
                errors.Add(new MultipleDeclarationsOfVariableInScope(locations: group.Select(v => (v.File, v.Line)),
                                                                     name: group.First().Name));
            }
        }


        private void CheckForInvalidAssignments(ISyntaxNode scope) {
            foreach (var assignment in scope.AllChildren.OfType<IAssignment>()) {
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
            var returns = function.AllChildren.OfType<ReturnStatementSyntax>().ToList();

            if (!(signature.ReturnType is VoidType) && returns.None()) {
                errors.Add(new MissingReturnStatement(function.File, function.Line));
            }

            foreach (var returnStatement in returns) {
                var returnType = returnStatement.IsVoid
                                     ? new VoidType()
                                     : BindExpression(returnStatement.ReturnValue!);

                if (!IsAssignableTo(returnType, signature.ReturnType)) {
                    errors.Add(new TypeMismatchInReturnStatement(file: returnStatement.File,
                                                                 line: returnStatement.Line,
                                                                 returned: returnType.Name,
                                                                 expected: signature.ReturnType.Name));
                }
            }

        }

    }

}