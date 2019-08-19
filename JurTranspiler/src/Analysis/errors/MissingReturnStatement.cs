using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

	public class MissingReturnStatement : SingleLocationError {

		public MissingReturnStatement(string file, int line) : base(file, line) {
		}


		protected override string MessageBody => "";

	}

}