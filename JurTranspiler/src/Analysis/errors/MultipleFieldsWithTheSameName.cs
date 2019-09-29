using System.Collections.Generic;
using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

	public class MultipleFieldsWithTheSameName : MultipleLocationError {

		private string name { get; }


		public MultipleFieldsWithTheSameName(IEnumerable<Location> locations, string name) : base(locations) {
			this.name = name;
		}


		protected override string MessageBody => $"Name: {name}";

	}

}