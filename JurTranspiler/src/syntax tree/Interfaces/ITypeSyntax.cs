using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.parsing.Implementations {

    public interface ITypeSyntax : ISyntaxNode {
        string Name { get; }
        string FullName { get; }
    }

}