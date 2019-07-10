using System.Collections.Generic;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.nodes {

    public interface IVariableDeclarationSyntax : ISyntaxNode {

        string Name { get; }

        //is null when inferred variable
        ITypeSyntax Type { get; }
    }

}