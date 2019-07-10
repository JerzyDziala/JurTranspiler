using System.Collections.Generic;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

    public class MultipleTypeParametersWithTheSameName : Error{


        private IEnumerable<(string file, int line)> locations;
        private string Name;


        public MultipleTypeParametersWithTheSameName(IEnumerable<(string file, int line)> locations, string name) {
            this.locations = locations;
            Name = name;
        }


        private string GetLocationsString() => $"{string.Join(",\\n", locations.Select(x => $"File: {x.file}, Line: {x.line}"))}";

        public override string GetMessage() => $"MultipleTypeParametersWithTheSameName ### Name: {Name}, Locations: {GetLocationsString()}";    }

}