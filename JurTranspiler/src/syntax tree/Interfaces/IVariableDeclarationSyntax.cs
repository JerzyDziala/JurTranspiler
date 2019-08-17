using JurTranspiler.compilerSource.parsing.Implementations;

namespace JurTranspiler.compilerSource.nodes {

    public interface IVariableDeclarationSyntax : ISyntaxNode {

        string Name { get; }

        //is null when inferred variable
        ITypeSyntax? Type { get; }
    }

}