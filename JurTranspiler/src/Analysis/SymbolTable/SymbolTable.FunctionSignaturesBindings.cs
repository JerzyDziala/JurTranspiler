using System.Collections.Generic;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model.functions;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable {
        public Dictionary<FunctionDefinitionSyntax, FunctionSignature> FunctionSignaturesBindings { get; } = new Dictionary<FunctionDefinitionSyntax, FunctionSignature>();
        public bool AlreadyBound(FunctionDefinitionSyntax syntax) => FunctionSignaturesBindings.ContainsKey(syntax);
        public FunctionSignature GetBindingFor(FunctionDefinitionSyntax syntax) => FunctionSignaturesBindings[syntax];


        public FunctionSignature MakeBindingFor(FunctionDefinitionSyntax syntax, FunctionSignature value) {
            FunctionSignaturesBindings[syntax] = value;
            return value;
        }
    }

}