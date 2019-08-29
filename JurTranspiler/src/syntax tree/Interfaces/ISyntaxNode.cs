using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.compilerSource.nodes {

    public interface ISyntaxNode : ITreeNode, IHaveLocation {

        int Abstraction { get; }
        string File { get; }
        int Line { get; }
        ISyntaxNode Root { get; }
        ISyntaxNode? Parent { get; }

        IFunctionDefinitionOrLambdaSyntax? EnclosingFunctionOrLambda { get; }
        ImmutableArray<ISyntaxNode> AllParents { get; }
        ImmutableArray<IVariableDeclarationSyntax> GetVisibleVariablesInScope();

        string ToJs(Knowledge knowledge);

    }

}