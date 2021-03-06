using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.Analysis.errors.bases;
using JurTranspiler.semantic_model.functions;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.declarations;
using UtilityLibrary;

namespace JurTranspiler.Analysis.Binder {

	public partial class Binder {

		private readonly SymbolTable.SymbolTable symbols;
		private readonly HashSet<Error> errors;

		public Knowledge Knowledge {
			get {

				GenerateNewCallableNames();
				GenerateNewVariableNames();

				//TODO: remove some expensive and unnecessary allocations
				var allTypesExplicitlyWrittenInTheProgram = symbols.TypesBindings.Values.ToImmutableArray();
				var allTypesExtractedFromStructDefinitions = symbols.OpenStructsBinding.Values.ToImmutableArray();
				var allTypesReturnedByExpressions = symbols.ExpressionsBindings.Values.ToImmutableArray();

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
				                                                    .Distinct(UtilityLibrary.Comparer<IType>.MakeComp((type, type1) => type.Name.Equals(type1.Name), type => type.Name.GetHashCode()))
				                                                    .ToImmutableArray();

				allTypes.OfType<StructType>()
				        .ToImmutableList()
				        .ForEach(x => BindFields(x));

				return symbols?.ToKnowledge(allTypes)
				    ?? throw new Exception("you tried to get knowledge before the symbolTable was initialized");
			}
		}


		public Binder(SyntaxTree tree, HashSet<Error> errors) {
			this.errors = errors;
			symbols = new SymbolTable.SymbolTable(tree);
			BindAllTheThings();
			PerformOtherChecks();
		}


		private void BindAllTheThings() {
			//bind structs, function declarations
			PerformDeclarationsPhase();

			//bind types
			foreach (var typeSyntax in symbols.Tree.AllTypeUsages) {
				var type = BindType(typeSyntax);
				if (type is StructType structType) BindFields(structType);
			}

			//bind expressions
			foreach (var expression in symbols.Tree.AllExpressions) {
				var returnType = BindExpression(expression);
				if (returnType is StructType structType) BindFields(structType);
			}

			//bind rest of bodies
			BindFunctionBodies();
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
			                              .GroupBy(x => x.NonGenericName + " " + x.Arity.ToString())
			                              .Where(x => x.MoreThenOne());
			foreach (var duplicate in structDuplicates) {
				errors.Add(new MultipleDeclarationsOfStruct(duplicate.Select(x => new Location(x.OriginalDefinitionSyntax.File, x.OriginalDefinitionSyntax.Line)),
				                                            duplicate.First().NonGenericName));
			}

			//of functions
			var functionsDuplicates = symbols.FunctionSignaturesBindings.Values
			                                 .GroupBy(x => x, UtilityLibrary.Comparer<FunctionSignature>.MakeComp((a, b) => a.IsConsideredSameAs(b), a => a.Name.GetHashCode()))
			                                 .Where(x => x.MoreThenOne())
			                                 .ToList();
			foreach (var duplicate in functionsDuplicates) {
				errors.Add(new MultipleDeclarationsOfFunction(duplicate.Select(x => new Location(x.OriginalDefinition.File, x.OriginalDefinition.Line)), duplicate.First().Name));
			}
		}

	}

}