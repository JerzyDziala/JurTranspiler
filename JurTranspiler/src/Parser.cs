using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using JurTranspiler.Analysis.errors.bases;
using JurTranspiler.ANTLR;
using JurTranspiler.syntax_tree.declarations;

namespace JurTranspiler {

    public static class Parser {

        public static SyntaxTree? ParseString(HashSet<Error> errors, string source) {

            var parser = new JurParser(new CommonTokenStream(new JurLexer(new AntlrInputStream(source))));

            parser.RemoveErrorListeners();
            parser.AddErrorListener(new ParserErrorListener("__TEST__", errors));

            var parsedProgram = parser.program();

            return errors.Any() ? null : new SyntaxTree(parsedProgram);
        }


        public static SyntaxTree? ParseFiles(HashSet<Error> errors, IEnumerable<(string code, string file)> files) {

            var parsedFiles = new List<(JurParser.ProgramContext, string)>();

            foreach (var (code, file) in files) {
                var parser = new JurParser(new CommonTokenStream(new JurLexer(new AntlrInputStream(code))));

                parser.RemoveErrorListeners();
                parser.AddErrorListener(new ParserErrorListener(file, errors));

                parsedFiles.Add((parser.program(), file));

            }
            return errors.Any() ? null : new SyntaxTree(parsedFiles);

        }

    }

}