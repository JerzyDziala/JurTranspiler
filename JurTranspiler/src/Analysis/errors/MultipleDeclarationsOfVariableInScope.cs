using System.Collections.Generic;
using System.Linq;

namespace JurTranspiler.compilerSource.Analysis {

    public class MultipleDeclarationsOfVariableInScope : Error {

        private IEnumerable<(string file, int line)> FilesLinesLocations;
        private string Name;


        public MultipleDeclarationsOfVariableInScope(IEnumerable<(string file, int line)> filesLinesLocations, string name) {
            FilesLinesLocations = filesLinesLocations.OrderBy(x=>x.file+x.line);
            Name = name;
        }


        private string GetLocationsString() => $"{string.Join(",\\n", FilesLinesLocations.Select(x => $"File: {x.file}, Line: {x.line}"))}";

        public override string GetMessage() => $"MultipleDeclarationsOfVariableInScope ### Name: {Name}, Locations: {GetLocationsString()}";
    }

}