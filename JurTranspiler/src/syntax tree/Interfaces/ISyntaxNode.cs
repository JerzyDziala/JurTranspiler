using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.compilerSource.nodes {

    public interface ISyntaxNode : ITreeNode {

        int Abstraction { get; }
        string File { get; }
        int Line { get; }
        ISyntaxNode Root { get; }
        ISyntaxNode? Parent { get; }
        ImmutableArray<ISyntaxNode> AllParents { get; }

        ImmutableArray<IVariableDeclarationSyntax> GetVisibleVariablesInScope();

        string ToJs(Knowledge knowledge);

    }

}