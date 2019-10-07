using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.semantic_model;
using JurTranspiler.syntax_tree.declarations;
using JurTranspiler.syntax_tree.expressions;
using JurTranspiler.syntax_tree.types;

namespace JurTranspiler.Analysis.SymbolTable {

    public partial class SymbolTable {

        public SyntaxTree Tree { get; }


        public SymbolTable(SyntaxTree tree) {
            Tree = tree;

        }


        //searching



    }

}