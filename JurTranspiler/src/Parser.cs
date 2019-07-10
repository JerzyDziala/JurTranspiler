using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;

namespace JurTranspiler.compilerSource {

    public static class Parser {

        public static SyntaxTree ParseString(HashSet<Error> errors, string source) {

            var parser = new JurParser(new CommonTokenStream(new JurLexer(new AntlrInputStream(source))));

            parser.RemoveErrorListeners();
            parser.AddErrorListener(new ParserErrorListener("__TEST__", errors));

            var parsedProgram = parser.program();

            return errors.Any() ? null : new SyntaxTree(parsedProgram);

        }
    }

}