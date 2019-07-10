using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

	public class UseOfUndeclaredFunction : SingleLocationError {

		public UseOfUndeclaredFunction(string file, int line, string fullName) : base(file, line) {
			this.fullName = fullName;
		}


		private string fullName;

		public override string GetMessage() => $"UseOfUndeclaredFunction ### {GetLocationString}, Name: {fullName}";

	}

}