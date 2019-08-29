using System.Collections.Generic;
using System.Linq;

namespace JurTranspiler.compilerSource.Analysis {

	public class MultipleDeclarationsOfStruct : MultipleLocationError {

		private string fieldName { get; }


		public MultipleDeclarationsOfStruct(IEnumerable<Location> locations, string fieldName) : base(locations) {
			this.fieldName = fieldName;
		}


		protected override string MessageBody => $"FieldName: {fieldName}";

	}

}