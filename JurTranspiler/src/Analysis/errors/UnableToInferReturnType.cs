using System.Collections.Generic;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

    public class UnableToInferReturnType : Error {

        private IEnumerable<(string file, int line)> locations;


        public UnableToInferReturnType(IEnumerable<(string file, int line)> locations) {
            this.locations = locations;
        }


        private string GetLocationsString() => $"{string.Join(",\\n", locations.Select(x => $"File: {x.file}, Line: {x.line}"))}";

        public override string GetMessage() => $"UnableToInferReturnType ### Locations: {GetLocationsString()}";
    }

}