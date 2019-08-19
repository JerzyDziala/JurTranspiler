using System;
using Antlr4.Runtime.Tree;
using JurTranspiler.src.syntax_tree.types;

namespace JurTranspiler.compilerSource.nodes {

    public static class Extensions {
        public static VoidTypeSyntax ToVoidType(this ITerminalNode voidNode, ISyntaxNode parent) {
            if (voidNode.Symbol.Text != "void") {
                throw new ArgumentException($"only use {nameof(ToVoidType)} on Void Token");
            }
            return new VoidTypeSyntax(parent, voidNode.Symbol.Line);
        }
    }

}