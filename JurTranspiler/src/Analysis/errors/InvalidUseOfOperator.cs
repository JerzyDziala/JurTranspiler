using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

	public class InvalidUseOfOperator : SingleLocationError {

		public string Op { get; }


		public InvalidUseOfOperator(Location location, string op) : base(location) {
			Op = op;
		}


		protected override string MessageBody => $"cannot use '{Op}'. this operator can only be used on variables";

	}

}