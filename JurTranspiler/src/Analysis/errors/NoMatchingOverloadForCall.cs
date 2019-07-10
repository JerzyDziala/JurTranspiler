using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

    public class NoMatchingOverloadForCall : SingleLocationError {

        private string callString;


        public NoMatchingOverloadForCall(string file, int line, string callString) : base(file, line) {
            this.callString = callString;
        }


        public override string GetMessage() => $"NoMatchingOverloadForCall ### {GetLocationString}, the arguments (or type arguments) in call {callString} doesn't match any available overload";
    }

}