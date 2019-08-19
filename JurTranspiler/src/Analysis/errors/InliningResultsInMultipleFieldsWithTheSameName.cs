using System.Collections.Generic;
using System.Linq;

namespace JurTranspiler.compilerSource.Analysis {

	public class InliningResultsInMultipleFieldsWithTheSameName : SingleLocationError {

		private string fieldName { get; }
		private IEnumerable<string> fieldOwnersNames { get; }


		public InliningResultsInMultipleFieldsWithTheSameName(string file,
		                                                      int line,
		                                                      string fieldName,
		                                                      IEnumerable<string> fieldOwnersNames) : base(file, line) {
			this.fieldName = fieldName;
			this.fieldOwnersNames = fieldOwnersNames;
		}


		protected override string MessageBody => $"types: {string.Join(", ", fieldOwnersNames.Select(name => $"\"{name}\""))} all have field named: \"{fieldName}\"";

	}

}