using System.Collections.Generic;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable {
        public Dictionary<IExpressionSyntax, Type> ExpressionsBindings { get; } = new Dictionary<IExpressionSyntax, Type>();
        public bool AlreadyBound(IExpressionSyntax syntax) => ExpressionsBindings.ContainsKey(syntax);
        public Type GetBindingFor(IExpressionSyntax syntax) => ExpressionsBindings[syntax];


        public Type MakeBindingFor(IExpressionSyntax syntax, Type value) {
            ExpressionsBindings[syntax] = value;
            return value;
        }
    }

}