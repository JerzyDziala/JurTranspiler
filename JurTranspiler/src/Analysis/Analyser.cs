using System.Collections.Generic;
using JurTranspiler.Analysis.errors.bases;
using JurTranspiler.syntax_tree.declarations;

namespace JurTranspiler.Analysis {

    public static class Analyser {

        public static Knowledge Analyse(HashSet<Error> errors, SyntaxTree tree) {

            var binder = new Binder.Binder(tree, errors);


            return binder.Knowledge;
        }

    }

}