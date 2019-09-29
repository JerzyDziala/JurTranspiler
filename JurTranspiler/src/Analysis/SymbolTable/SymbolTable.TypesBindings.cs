using System.Collections.Generic;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.Analysis.SymbolTable {

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