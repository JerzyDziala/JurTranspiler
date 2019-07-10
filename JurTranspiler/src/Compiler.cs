using System.Collections.Generic;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using UtilityLibrary;

namespace JurTranspiler.compilerSource
{

    public static class Compiler
    {

        public static (HashSet<Error> diagnostics, string jsCode) Compile(string jurCode)
        {

            var diagnostics = new HashSet<Error>();

            var tree = Parser.ParseString(diagnostics, jurCode);

            if (diagnostics.Any())
                return (diagnostics, string.Empty);

            var binder = Analyser.Analyse(diagnostics, tree);

			return diagnostics.None() ? (diagnostics, tree.ToJs(binder)) : (diagnostics, "");
//            return (diagnostics, "");

        }

    }

}