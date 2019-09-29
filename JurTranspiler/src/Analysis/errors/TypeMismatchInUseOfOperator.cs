using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

	public class TypeMismatchInUseOfOperator : SingleLocationError {

		public TypeMismatchInUseOfOperator(string file,
		                                   int line,
		                                   string op,
		                                   string leftName,
		                                   string? rightName = null) : base(file, line) {
			this.op = op;
			this.leftName = leftName;
			this.rightName = rightName;
		}


		public TypeMismatchInUseOfOperator(Location location,
		                                   string op,
		                                   string leftName,
		                                   string? rightName = null) : base(location) {
			this.op = op;
			this.leftName = leftName;
			this.rightName = rightName;
		}


		private string op;
		private string leftName;
		private string? rightName;

		protected override string MessageBody => rightName is null ?
			                                         $"cannot perform operation: '{op}' on value of type '{leftName}'"
			                                         : $"cannot perform operation: '{op}' on values of type '{leftName}' and '{rightName}'";

	}

}