namespace JurTranspiler.compilerSource.Analysis {

	public class InvalidAssignment : SingleLocationError {

		public InvalidAssignment(Location location) : base(location) {
		}


		protected override string MessageBody => $"cannot assign to a right hand expression";

	}

}