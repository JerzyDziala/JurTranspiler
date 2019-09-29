using System.Collections.Generic;
using JurTranspiler.semantic_model.functions;
using JurTranspiler.syntax_tree.declarations;

namespace JurTranspiler.Analysis.SymbolTable {

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