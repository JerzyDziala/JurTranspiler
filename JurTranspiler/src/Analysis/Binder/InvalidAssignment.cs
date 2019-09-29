using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.Binder {

	public class InvalidAssignment : SingleLocationError {

		public InvalidAssignment(Location location) : base(location) {
		}


		protected override string MessageBody => $"cannot assign to a right hand expression";

	}

}