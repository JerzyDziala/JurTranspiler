using System.Collections.Generic;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable {
        public Dictionary<ITypeSyntax, Type> TypesBindings { get; } = new Dictionary<ITypeSyntax, Type>();
        public bool AlreadyBound(ITypeSyntax syntax) => TypesBindings.ContainsKey(syntax);
        public Type GetBindingFor(ITypeSyntax syntax) => TypesBindings[syntax];


        public Type MakeBindingFor(ITypeSyntax syntax, Type value) {
            if (!(value is UndeclaredStructType)) {
                TypesBindings[syntax] = value;
            }
            return value;
        }
    }

}