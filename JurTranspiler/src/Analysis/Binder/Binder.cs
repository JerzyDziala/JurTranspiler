using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.compilerSource.semantic_model.functions;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {

        private SymbolTable symbols;
        private HashSet<Error> errors;

        public Knowledge Knowledge {
            get {

                //TODO: remove some expensive and unnecessary allocations
                var allTypesExplicitlyWrittenInTheProgram = symbols.TypesBindings.Values.ToImmutableArray();
                var allTypesExtractedFromStructDefinitions = symbols.OpenStructsBinding.Values.ToImmutableArray();
                var allTypesReturnedByExpressions = symbols.ExpressionsBindings.Values.ToImmutableArray();

                allTypesExplicitlyWrittenInTheProgram.OfType<StructType>()
                                                     .Select(BindFields)
                                                     .ToImmutableArray();

                var allPostSubstitutionFieldsTypes = allTypesReturnedByExpressions.Concat(allTypesExplicitlyWrittenInTheProgram)
                                                                                  .OfType<StructType>()
                                                                                  .SelectMany(x => BindFields(x).Select(y => y.Type))
                                                                                  .ToImmutableArray();

                var allPreSubstitutionFieldsTypes = allTypesExtractedFromStructDefinitions.OfType<StructType>()
                                                                                          .SelectMany(x => BindFields(x).Select(y => y.Type))
                                                                                          .ToImmutableArray();

                var allTypes = allTypesExplicitlyWrittenInTheProgram.Concat(allTypesExtractedFromStructDefinitions)
                                                                    .Concat(allTypesReturnedByExpressions)
                                                                    .Concat(allPostSubstitutionFieldsTypes)
                                                                    .Concat(allPreSubstitutionFieldsTypes)
                                                                    .Distinct();
                return symbols?.ToKnowledge(allTypes)
                    ?? throw new Exception("you tried to get knowledge before the symbolTable was initialized");
            }
        }


        public Binder(SyntaxTree tree, HashSet<Error> errors) {
            this.errors = errors;
            symbols = new SymbolTable(tree);
            BindAllTheThings();
        }


        private void BindAllTheThings() {
            //bind structs, function declarations
            PerformDeclarationsPhase();

            //bind types
            foreach (var typeSyntax in symbols.Tree.AllTypeUsages) {
                var type = BindType(typeSyntax);
                if (type is StructType structType) BindFields(structType);
            }

            //bind expressions (must be last)
            foreach (var expression in symbols.Tree.AllExpressions) {
                var returnType = BindExpression(expression);
                if (returnType is StructType structType) BindFields(structType);
            }
        }


        private void PerformDeclarationsPhase() {
            //bind struct definitions
            foreach (var definitionSyntax in symbols.Tree.AllStructDefinitions) {
                BindStructDefinition(definitionSyntax);
            }
            foreach (var boundDefinition in symbols.OpenStructsBinding.Values) {
                if (boundDefinition is StructType type) BindFields(type);
            }

            //bind function definitions
            foreach (var definitionSyntax in symbols.Tree.AllFunctionDefinitions) {
                BindFunctionDefinition(definitionSyntax);
            }

            //check for duplicates of structs
            var structDuplicates = symbols.OpenStructsBinding.Values
                                          .OfType<StructType>()
                                          .GroupBy(x => x.NonGenericName + " " + x.Arity)
                                          .Where(x => x.MoreThenOne());
            foreach (var duplicate in structDuplicates) {
                errors.Add(new MultipleDeclarationsOfStruct(duplicate.Select(x => (x.OriginalDefinitionSyntax.File, x.OriginalDefinitionSyntax.Line)),
                                                            duplicate.First().NonGenericName));
            }

            //of functions
            var functionsDuplicates = symbols.FunctionSignaturesBindings.Values
                                             .GroupBy(x => x, UtilityLibrary.Comparer<FunctionSignature>.MakeComp((a, b) => a.IsConsideredSameAs(b), a => a.Name.GetHashCode()))
                                             .Where(x => x.MoreThenOne())
                                             .ToList();
            foreach (var duplicate in functionsDuplicates) {
                errors.Add(new MultipleDeclarationsOfFunction(duplicate.Select(x => (x.OriginalDefinition.File, x.OriginalDefinition.Line)), duplicate.First().Name));
            }
        }

    }

}