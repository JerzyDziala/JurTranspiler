using System.Collections.Generic;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable {

        public Dictionary<(IType, IType), bool> AssignabilityBindings { get; } = new Dictionary<(IType, IType), bool>();
        public bool AlreadyBound((IType, IType) types) => AssignabilityBindings.ContainsKey(types);
        public bool GetBindingFor((IType, IType) types) => AssignabilityBindings[types];


        public bool MakeBindingFor((IType, IType) types, bool value) {
            AssignabilityBindings[types] = value;
            return value;
        }

    }

}