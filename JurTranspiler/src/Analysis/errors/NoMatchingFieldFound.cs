using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

	public class NoMatchingFieldFound : SingleLocationError {

		private string fieldName { get; }
		private string typeName { get; }


		public NoMatchingFieldFound(string file,
		                            int line,
		                            string fieldName,
		                            string typeName) : base(file, line) {
			this.fieldName = fieldName;
			this.typeName = typeName;
		}


		public NoMatchingFieldFound(Location location, string fieldName, string typeName) : base(location) {
			this.fieldName = fieldName;
			this.typeName = typeName;
		}


		protected override string MessageBody => $"FieldName: {fieldName}, on type: {typeName}";

	}

}