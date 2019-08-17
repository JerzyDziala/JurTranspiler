using System.Collections.Generic;
using System.Linq;

namespace JurTranspiler.compilerSource.Analysis {

    public class MultipleFieldsWithTheSameName : Error {

        private IEnumerable<(string file, int line)> FilesLinesLocations;
        private string Name;


        public MultipleFieldsWithTheSameName(IEnumerable<(string file, int line)> filesLinesLocations, string name) {
            FilesLinesLocations = filesLinesLocations;
            Name = name;
        }


        private string GetLocationsString() => $"{string.Join(",\\n", FilesLinesLocations.Select(x => $"File: {x.file}, Line: {x.line}"))}";

        public override string GetMessage() => $"MultipleFieldsWithTheSameName ### Name: {Name}, Locations: {GetLocationsString()}";
    }

}