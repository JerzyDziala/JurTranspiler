using System.Collections.Generic;
using System.Linq;

namespace JurTranspiler.compilerSource.Analysis {

	public abstract class MultipleLocationError : Error {

		protected override string Localization => $"{string.Join(",\\n", locations.Select(x => $"File: {x.file}, Line: {x.line}"))}";
		protected IEnumerable<(string file, int line)> locations { get; }


		protected MultipleLocationError(IEnumerable<(string file, int line)> locations) {
			this.locations = locations.OrderBy(x => x.file + x.line);
		}

	}

}