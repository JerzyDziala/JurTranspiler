using System.Collections.Generic;
using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

	public class MultipleDeclarationsOfStruct : MultipleLocationError {

		private string fieldName { get; }


		public MultipleDeclarationsOfStruct(IEnumerable<Location> locations, string fieldName) : base(locations) {
			this.fieldName = fieldName;
		}


		protected override string MessageBody => $"FieldName: {fieldName}";

	}

}