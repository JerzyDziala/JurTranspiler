using JurTranspiler.compilerSource.nodes;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.parsing.Implementations {

    public interface ITypeSyntax : ISyntaxNode {
        string Name { get; }
        string FullName { get; }
    }

}