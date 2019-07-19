using System.Collections.Generic;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable {

        public Dictionary<StructDefinitionSyntax, Type> OpenStructsBinding { get; } = new Dictionary<StructDefinitionSyntax, Type>();
        public bool AlreadyBound(StructDefinitionSyntax syntax) => OpenStructsBinding.ContainsKey(syntax);
        public Type GetBindingFor(StructDefinitionSyntax syntax) => OpenStructsBinding[syntax];


        public Type MakeBindingFor(StructDefinitionSyntax syntax, Type value) {
            OpenStructsBinding[syntax] = value;
            return value;
        }
    }

}