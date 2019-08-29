using System.Collections.Generic;
using System.Linq;

namespace JurTranspiler.compilerSource.Analysis {

	public class MultipleDeclarationsOfVariableInScope : MultipleLocationError {

		private string name { get; }


		public MultipleDeclarationsOfVariableInScope(IEnumerable<Location> locations, string name) : base(locations) {
			this.name = name;
		}


		protected override string MessageBody => $"Name: {name}";

	}

}