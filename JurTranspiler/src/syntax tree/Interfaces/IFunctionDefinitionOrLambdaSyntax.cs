using System.Collections.Immutable;

namespace JurTranspiler.compilerSource.nodes {

    public interface IFunctionDefinitionOrLambdaSyntax : ISyntaxNode{
        bool IsArrow { get; }
        bool IsExtern { get; }
        ImmutableArray<ReturnStatementSyntax> TopLevelReturns { get; }
        IStatementSyntax? Body { get; }
        ImmutableArray<UninitializedVariableDeclarationSyntax> Parameters { get; }
    }

}