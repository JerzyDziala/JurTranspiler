using System;
using System.Collections.Generic;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.CodeGeneration;
using UtilityLibrary;

namespace JurTranspiler.compilerSource {

    public static class Compiler {

        public static (HashSet<Error> diagnostics, string jsCode) Compile(string jurCode) {

            var diagnostics = new HashSet<Error>();

            var tree = Parser.ParseString(diagnostics, jurCode);

            if (tree == null) return (diagnostics, string.Empty);

            var knowledge = Analyser.Analyse(diagnostics, tree);

            if (diagnostics.Any()) return (diagnostics, string.Empty);

            var jsCode = new Generator(knowledge, tree).GenerateJs();

            if (diagnostics.Any()) throw new Exception($"code generation found errors:\n{diagnostics.Select(x => x.GetMessage()).Glue("\n")}");

            return (diagnostics, jsCode);
        }


        public static (HashSet<Error> diagnostics, string jsCode) Compile(IEnumerable<(string code, string file)> jurCode) {

            var diagnostics = new HashSet<Error>();

            var tree = Parser.ParseFiles(diagnostics, jurCode);

            if (tree == null) return (diagnostics, string.Empty);

            var knowledge = Analyser.Analyse(diagnostics, tree);

            if (diagnostics.Any()) return (diagnostics, string.Empty);

            var jsCode = new Generator(knowledge, tree).GenerateJs();

            if (diagnostics.Any()) throw new Exception($"code generation found errors:\n{diagnostics.Select(x => x.GetMessage()).Glue("\n")}");

            return (diagnostics, jsCode);
        }
    }

}