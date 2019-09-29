using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

	public class IndexAccessOnNonArray : SingleLocationError {

		private string TypeName { get; }


		public IndexAccessOnNonArray(string file, int line, string typeName) : base(file, line) {
			TypeName = typeName;
		}


		protected override string MessageBody => "type: " + TypeName;

	}

}