using System.Collections.Generic;
using System.Linq;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.compilerSource.semantic_model.functions;
using UtilityLibrary;
using static UtilityLibrary.Comparer<JurTranspiler.compilerSource.semantic_model.functions.FunctionSignature>;

namespace JurTranspiler.compilerSource.Analysis {

	public partial class Binder {

		private SymbolTable symbols;


		public Binder(SyntaxTree tree) {
			symbols = new SymbolTable(tree);
		}


		public void BindAllTheThings(HashSet<Error> errors) {
			//bind structs, function declarations
			PerformDeclarationsPhase(errors);

			//bind types
			foreach (var typeSyntax in symbols.typeUsages) {
				BindType(typeSyntax, errors);
			}

			//bind expressions (must be last)
			foreach (var expression in symbols.expressions) {
				expression.Evaluate(errors, this);
			}
		}


		private void PerformDeclarationsPhase(HashSet<Error> errors) {
			//bind struct definitions
			foreach (var definitionSyntax in symbols.structDefinitions) {
				BindStructDefinition(definitionSyntax, errors);
			}
			foreach (var boundDefinition in symbols.OpenStructsBinding.Values) {
				if (boundDefinition is StructType type) type.GetAllFields(errors);
			}

			//bind function definitions
			foreach (var definitionSyntax in symbols.functionDefinitions) {
				BindFunctionDefinition(definitionSyntax, errors);
			}

			//check for duplicates
			//of structs
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
			                                 .GroupBy(x => x, MakeComp<FunctionSignature>((a, b) => a.IsConsideredSameAs(b), a => a.Name.GetHashCode()))
			                                 .Where(x => x.MoreThenOne())
			                                 .ToList();
			foreach (var duplicate in functionsDuplicates) {
				errors.Add(new MultipleDeclarationsOfFunction(duplicate.Select(x => (x.OriginalDefinition.File, x.OriginalDefinition.Line)), duplicate.First().Name));
			}
		}
	}

}