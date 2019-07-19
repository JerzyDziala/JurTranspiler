using System.Collections.Generic;
using JurTranspiler.compilerSource.semantic_model.functions;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable{

        public Dictionary<ICallable, string> NewNames = new Dictionary<ICallable, string>();
    }

}