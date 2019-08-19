using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

	public class IndexAccessOnNonArray : SingleLocationError {

		public IndexAccessOnNonArray(string file, int line) : base(file, line) {
		}


		protected override string MessageBody => "";

	}

}