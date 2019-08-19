using System.Collections.Generic;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

	public class MultipleTypeParametersWithTheSameName : MultipleLocationError {

		private string name { get; }


		public MultipleTypeParametersWithTheSameName(IEnumerable<(string file, int line)> locations, string name) : base(locations) {
			this.name = name;
		}


		protected override string MessageBody => $"Name: {name}";

	}

}