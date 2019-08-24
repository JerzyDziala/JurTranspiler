using System.Collections.Generic;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model.functions;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable {

        public Dictionary<ICallable, string> NewFunctionNames = new Dictionary<ICallable, string>();
        public Dictionary<IVariableDeclarationSyntax, string> NewVariableNames = new Dictionary<IVariableDeclarationSyntax, string>();
    }

}