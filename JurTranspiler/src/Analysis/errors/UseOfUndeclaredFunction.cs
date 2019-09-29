using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

	public class UseOfUndeclaredFunction : SingleLocationError {

		public UseOfUndeclaredFunction(string file, int line, string fullName) : base(file, line) {
			this.fullName = fullName;
		}


		private string fullName;

		protected override string MessageBody => $"Name: {fullName}";

	}

}