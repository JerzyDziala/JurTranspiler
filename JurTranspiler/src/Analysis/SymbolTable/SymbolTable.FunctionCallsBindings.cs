using System.Collections.Generic;
using JurTranspiler.semantic_model;
using JurTranspiler.syntax_tree.expressions;

namespace JurTranspiler.Analysis.SymbolTable {

    public partial class SymbolTable {
        public Dictionary<FunctionCallSyntax, FunctionCallInfo> FunctionCallsBindings { get; } = new Dictionary<FunctionCallSyntax, FunctionCallInfo>();
        public bool AlreadyBound(FunctionCallSyntax syntax) => FunctionCallsBindings.ContainsKey(syntax);
        public FunctionCallInfo GetBindingFor(FunctionCallSyntax syntax) => FunctionCallsBindings[syntax];


        public FunctionCallInfo MakeBindingFor(FunctionCallSyntax syntax, FunctionCallInfo value) {
            FunctionCallsBindings[syntax] = value;
            return value;
        }
    }

}