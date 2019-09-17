using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

	public class InvalidUseOfNew : SingleLocationError {

		public string typeName { get; }


		public InvalidUseOfNew(string file, int line, string typeName) : base(file, line) {
			this.typeName = typeName;
		}


		public InvalidUseOfNew(Location location, string typeName) : base(location) {
			this.typeName = typeName;
		}


		protected override string MessageBody => $"cannot use 'new' on type '{typeName}'. 'new' can only be used on structs or arrays";

	}

}