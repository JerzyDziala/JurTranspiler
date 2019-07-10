using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
namespace JurTranspiler.compilerSource.nodes {

    public interface ISyntaxNode : ITreeNode {

        int Abstraction { get; }
        string File { get; }
        int Line { get; }
        ISyntaxNode Root { get; }
        ISyntaxNode Parent { get; }
        ImmutableList<ISyntaxNode> AllParents { get; }

        string ToJs(Binder binder);

    }

}