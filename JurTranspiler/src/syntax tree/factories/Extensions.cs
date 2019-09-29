using System;
using Antlr4.Runtime.Tree;
using JurTranspiler.syntax_tree.Interfaces;
using JurTranspiler.syntax_tree.types;

namespace JurTranspiler.syntax_tree.factories {

    public static class Extensions {
        public static VoidTypeSyntax ToVoidType(this ITerminalNode voidNode, ISyntaxNode parent) {
            if (voidNode.Symbol.Text != "void") {
                throw new ArgumentException($"only use {nameof(ToVoidType)} on Void Token");
            }
            return new VoidTypeSyntax(parent, voidNode.Symbol.Line);
        }
    }

}