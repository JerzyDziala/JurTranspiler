using System.Collections.Generic;
using System.Linq;

namespace JurTranspiler.compilerSource.Analysis {

	public class MultipleDeclarationsOfFunction : MultipleLocationError {

		private string name { get; }


		public MultipleDeclarationsOfFunction(IEnumerable<(string file, int line)> locations, string name) : base(locations) {
			this.name = name;
		}


		protected override string MessageBody => $"Name: {name}";

	}

}