using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

    public class CallArgumentsViolateParametersConstraints : SingleLocationError {

        private string callString;
        public CallArgumentsViolateParametersConstraints(string file, int line, string callString) : base(file, line) {
            this.callString = callString;
        }


        public override string GetMessage() => $"FunctionCallArgumentsViolateGenericParametersConstraints ## {GetLocationString}, call: {callString}";
    }


}