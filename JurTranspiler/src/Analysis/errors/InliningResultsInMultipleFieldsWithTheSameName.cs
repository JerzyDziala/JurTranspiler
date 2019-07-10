using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace JurTranspiler.compilerSource.Analysis {

    public class InliningResultsInMultipleFieldsWithTheSameName : SingleLocationError {

        private string Name;
        private IEnumerable<string> FieldOwnersNames;


        public InliningResultsInMultipleFieldsWithTheSameName(string file,
                                                              int line,
                                                              string name,
                                                              IEnumerable<string> fieldOwnersNames) : base(file, line) {
            Name = name;
            FieldOwnersNames = fieldOwnersNames;
        }


        public override string GetMessage() => $"InliningResultsInMultipleFieldsWithTheSameName ### {GetLocationString} ### types: {string.Join(", ", FieldOwnersNames.Select(name => $"\"{name}\""))} all have field named: \"{Name}\"";

    }

}