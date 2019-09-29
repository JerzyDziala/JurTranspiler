using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.semantic_model;
using JurTranspiler.semantic_model.types;

namespace JurTranspiler.Analysis.SymbolTable {

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