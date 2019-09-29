using System.Collections.Immutable;
using JurTranspiler.syntax_tree.declarations;
using JurTranspiler.syntax_tree.statements;

namespace JurTranspiler.syntax_tree.Interfaces {

    public interface IFunctionDefinitionOrLambdaSyntax : ISyntaxNode{
        bool IsArrow { get; }
        bool IsExtern { get; }
        ImmutableArray<ReturnStatementSyntax> TopLevelReturns { get; }
        IStatementSyntax? Body { get; }
        ImmutableArray<UninitializedVariableDeclarationSyntax> Parameters { get; }
    }

}