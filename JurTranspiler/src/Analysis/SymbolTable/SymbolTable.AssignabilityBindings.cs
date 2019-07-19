using System.Collections.Generic;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable {

        public Dictionary<(Type, Type), bool> AssignabilityBindings { get; } = new Dictionary<(Type, Type), bool>();
        public bool AlreadyBound((Type, Type) types) => AssignabilityBindings.ContainsKey(types);
        public bool GetBindingFor((Type, Type) types) => AssignabilityBindings[types];


        public bool MakeBindingFor((Type, Type) types, bool value) {
            AssignabilityBindings[types] = value;
            return value;
        }

    }

}