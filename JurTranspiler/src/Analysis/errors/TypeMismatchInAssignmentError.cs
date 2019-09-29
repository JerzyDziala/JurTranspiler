using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

	public class TypeMismatchInAssignmentError : SingleLocationError {

		public TypeMismatchInAssignmentError(string file,
		                                     int line,
		                                     string leftName,
		                                     string rightName) : base(file, line) {
			this.leftName = leftName;
			this.rightName = rightName;
		}


		public TypeMismatchInAssignmentError(Location location,
		                                     string leftName,
		                                     string rightName) : base(location) {
			this.leftName = leftName;
			this.rightName = rightName;
		}


		private readonly string leftName;
		private readonly string rightName;

		protected override string MessageBody => $"cannot assign value of type {rightName} to an expression of type {leftName}";

	}

}