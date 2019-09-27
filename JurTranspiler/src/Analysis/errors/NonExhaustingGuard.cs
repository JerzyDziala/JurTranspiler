using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

	public class NonExhaustingGuard : SingleLocationError {

		public NonExhaustingGuard(string file, int line) : base(file, line) {
		}


		public NonExhaustingGuard(Location location) : base(location) {
		}


		protected override string MessageBody => $"guard expression has to have an 'otherwise' branch";

	}

}