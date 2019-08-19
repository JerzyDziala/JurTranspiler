using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

	public class CallArgumentsViolateParametersConstraints : SingleLocationError {

		private string callString { get; }


		public CallArgumentsViolateParametersConstraints(string file, int line, string callString) : base(file, line) {
			this.callString = callString;
		}


		protected override string MessageBody => $"call: {callString}";

	}

}