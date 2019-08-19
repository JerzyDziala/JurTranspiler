using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

	public class UnableToInferReturnType : MultipleLocationError {

		public UnableToInferReturnType(IEnumerable<(string file, int line)> locations) : base(locations) {
		}


		protected override string MessageBody => $"";

	}

}