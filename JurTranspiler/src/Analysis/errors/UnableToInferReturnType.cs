using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

	public class UnableToInferReturnType : MultipleLocationError {

		public UnableToInferReturnType(IEnumerable<Location> locations) : base(locations) {
		}


		protected override string MessageBody => $"";

	}

}