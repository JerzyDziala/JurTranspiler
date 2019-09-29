using System.Collections.Immutable;
using JurTranspiler.Analysis;

namespace JurTranspiler.syntax_tree.Interfaces {

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