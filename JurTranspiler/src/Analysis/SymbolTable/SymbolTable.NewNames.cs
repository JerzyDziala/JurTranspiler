using System.Collections.Generic;
using JurTranspiler.semantic_model.functions;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.Analysis.SymbolTable {

    public partial class SymbolTable {

        public Dictionary<ICallable, string> NewFunctionNames = new Dictionary<ICallable, string>();
        public Dictionary<IVariableDeclarationSyntax, string> NewVariableNames = new Dictionary<IVariableDeclarationSyntax, string>();
    }

}