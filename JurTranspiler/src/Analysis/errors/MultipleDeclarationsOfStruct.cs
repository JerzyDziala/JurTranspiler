using System.Collections.Generic;
using System.Linq;

namespace JurTranspiler.compilerSource.Analysis {

    public class MultipleDeclarationsOfStruct : Error {

        private IEnumerable<(string file, int line)> locations;
        private string Name;


        public MultipleDeclarationsOfStruct(IEnumerable<(string file, int line)> locations, string name) {
            this.locations = locations;
            Name = name;
        }


        private string GetLocationsString() => $"{string.Join(",\\n", locations.Select(x => $"File: {x.file}, Line: {x.line}"))}";

        public override string GetMessage() => $"MultipleDeclarationsOfStructError ### Name: {Name}, Locations: {GetLocationsString()}";
    }

}