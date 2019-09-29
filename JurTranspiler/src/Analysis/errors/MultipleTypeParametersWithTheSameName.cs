using System.Collections.Generic;
using System.Linq;
using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

	public class MultipleTypeParametersWithTheSameName : MultipleLocationError {

		private string name { get; }


		public MultipleTypeParametersWithTheSameName(IEnumerable<Location> locations, string name) : base(locations) {
			this.name = name;
		}


		protected override string MessageBody => $"Name: {name}";

	}

}