using System.Collections.Generic;
using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

	public class MultipleDeclarationsOfVariableInScope : MultipleLocationError {

		private string name { get; }


		public MultipleDeclarationsOfVariableInScope(IEnumerable<Location> locations, string name) : base(locations) {
			this.name = name;
		}


		protected override string MessageBody => $"Name: {name}";

	}

}