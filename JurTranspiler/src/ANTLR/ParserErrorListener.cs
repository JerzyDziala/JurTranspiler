using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Dfa;
using Antlr4.Runtime.Sharpen;
using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.compilerSource.parsing.Implementations {

    public class ParserErrorListener : BaseErrorListener {

        private string file;

        private HashSet<Error> diagnostics;


        public ParserErrorListener(string file, HashSet<Error> errors) {
            this.file = file;
            diagnostics = errors;
        }


        public override void SyntaxError(TextWriter output,
                                         IRecognizer recognizer,
                                         IToken offendingSymbol,
                                         int line,
                                         int charPositionInLine,
                                         string msg,
                                         RecognitionException e) {
            diagnostics.Add(new SyntaxError(file, line, msg));

        }


        public override void ReportAmbiguity(Antlr4.Runtime.Parser recognizer,
                                             DFA dfa,
                                             int startIndex,
                                             int stopIndex,
                                             bool exact,
                                             BitSet ambigAlts,
                                             ATNConfigSet configs) {

        }


        public override void ReportAttemptingFullContext(Antlr4.Runtime.Parser recognizer,
                                                         DFA dfa,
                                                         int startIndex,
                                                         int stopIndex,
                                                         BitSet conflictingAlts,
                                                         SimulatorState conflictState) {

        }


        public override void ReportContextSensitivity(Antlr4.Runtime.Parser recognizer,
                                                      DFA dfa,
                                                      int startIndex,
                                                      int stopIndex,
                                                      int prediction,
                                                      SimulatorState acceptState) {
        }

    }

}