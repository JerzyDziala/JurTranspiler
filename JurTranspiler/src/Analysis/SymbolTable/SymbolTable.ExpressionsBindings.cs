using System.Collections.Generic;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable {
        public Dictionary<IExpressionSyntax, IType> ExpressionsBindings { get; } = new Dictionary<IExpressionSyntax, IType>();
        public bool AlreadyBound(IExpressionSyntax syntax) => ExpressionsBindings.ContainsKey(syntax);
        public IType GetBindingFor(IExpressionSyntax syntax) => ExpressionsBindings[syntax];


        public IType MakeBindingFor(IExpressionSyntax syntax, IType value) {
            ExpressionsBindings[syntax] = value;
            return value;
        }
    }

}