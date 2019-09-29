using System.Collections.Generic;
using System.Linq;

namespace JurTranspiler.Analysis.errors.bases {

    public abstract class MultipleLocationError : Error {

        protected override string Localization => $"{string.Join(",\\n", locations.Select(x => $"File: {x.File}, Line: {x.Line}"))}";
        protected IEnumerable<Location> locations { get; }


        protected MultipleLocationError(IEnumerable<Location> locations) {
            this.locations = locations.OrderBy(x => x.File + x.Line);
        }

    }

}