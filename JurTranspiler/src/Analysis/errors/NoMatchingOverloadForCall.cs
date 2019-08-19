using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

    public class NoMatchingOverloadForCall : SingleLocationError {

        private string callString { get; }


        public NoMatchingOverloadForCall(string file, int line, string callString) : base(file, line) {
            this.callString = callString;
        }


        protected override string MessageBody => $"the arguments (or type arguments) in call {callString} doesn't match any available overload";

    }

}