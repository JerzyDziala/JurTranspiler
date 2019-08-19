using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

	public class AmbiguousFieldReference : SingleLocationError {

		private string fieldName { get; }
		private string typeName { get; }


		public AmbiguousFieldReference(string file,
		                               int line,
		                               string fieldName,
		                               string typeName) : base(file, line) {
			this.fieldName = fieldName;
			this.typeName = typeName;
		}


		protected override string MessageBody => $"FieldName: {fieldName}, on type: {typeName}";

	}

}