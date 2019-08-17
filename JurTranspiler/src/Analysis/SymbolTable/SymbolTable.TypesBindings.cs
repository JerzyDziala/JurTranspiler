using System.Collections.Generic;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable {
        public Dictionary<ITypeSyntax, IType> TypesBindings { get; } = new Dictionary<ITypeSyntax, IType>();
        public bool AlreadyBound(ITypeSyntax syntax) => TypesBindings.ContainsKey(syntax);
        public IType GetBindingFor(ITypeSyntax syntax) => TypesBindings[syntax];


        public IType MakeBindingFor(ITypeSyntax syntax, IType value) {
            if (!(value is UndeclaredStructType)) {
                TypesBindings[syntax] = value;
            }
            return value;
        }
    }

}