using System.Collections.Generic;
using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.Analysis {

	public static class Analyser {

		public static Knowledge Analyse(HashSet<Error> errors, SyntaxTree tree) {

			var binder = new Binder(tree, errors);


			binder.GenerateNewCallableNames();
			return binder.Knowledge;
		}

	}

}