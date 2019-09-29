using System.Collections.Generic;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.Analysis.SymbolTable {

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