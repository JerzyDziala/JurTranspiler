using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable {
        public Dictionary<StructType, ImmutableArray<Field>> FieldsBindings { get; } = new Dictionary<StructType, ImmutableArray<Field>>();
        public bool AlreadyBound(StructType type) => FieldsBindings.ContainsKey(type);
        public IEnumerable<Field> GetBindingFor(StructType type) => FieldsBindings[type];


        public IEnumerable<Field> MakeBindingFor(StructType type, ICollection<Field> value) {
            FieldsBindings[type] = value.ToImmutableArray();
            return FieldsBindings[type];
        }
    }

}