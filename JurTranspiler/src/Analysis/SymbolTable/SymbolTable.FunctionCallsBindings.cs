using System.Collections.Generic;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model.functions;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable {
        public Dictionary<FunctionCallSyntax, ICallable> FunctionCallsBindings { get; } = new Dictionary<FunctionCallSyntax, ICallable>();
        public bool AlreadyBound(FunctionCallSyntax syntax) => FunctionCallsBindings.ContainsKey(syntax);
        public ICallable GetBindingFor(FunctionCallSyntax syntax) => FunctionCallsBindings[syntax];


        public ICallable MakeBindingFor(FunctionCallSyntax syntax, ICallable value) {
            FunctionCallsBindings[syntax] = value;
            return value;
        }
    }

}