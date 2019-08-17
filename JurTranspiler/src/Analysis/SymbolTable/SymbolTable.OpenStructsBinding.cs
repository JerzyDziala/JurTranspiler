using System.Collections.Generic;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.Analysis {

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