using System.Collections.Generic;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.declarations;

namespace JurTranspiler.Analysis.SymbolTable {

    public partial class SymbolTable {

        public Dictionary<StructDefinitionSyntax, IType> OpenStructsBinding { get; } = new Dictionary<StructDefinitionSyntax, IType>();
        public bool AlreadyBound(StructDefinitionSyntax syntax) => OpenStructsBinding.ContainsKey(syntax);
        public IType GetBindingFor(StructDefinitionSyntax syntax) => OpenStructsBinding[syntax];


        public IType MakeBindingFor(StructDefinitionSyntax syntax, IType value) {
            OpenStructsBinding[syntax] = value;
            return value;
        }
    }

}