using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

	public class TypeMismatchInReturnStatement : SingleLocationError {

		public string returned { get; }
		public string expected { get; }


		public TypeMismatchInReturnStatement(string file, int line, string returned, string expected) : base(file, line) {
			this.returned = returned;
			this.expected = expected;
		}


		public override string GetMessage() => $" ###TypeMismatchInReturnStatement {GetLocationString}, returned type {returned} is not compatible with expected type {expected}";

	}

}