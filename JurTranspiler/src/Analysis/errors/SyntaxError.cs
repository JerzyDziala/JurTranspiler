using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

	public class SyntaxError : SingleLocationError {

		private string details { get; }


		public SyntaxError(string file, int line, string details) : base(file, line) {
			this.details = details;
		}


		protected override string MessageBody => $"{details}";

	}

}