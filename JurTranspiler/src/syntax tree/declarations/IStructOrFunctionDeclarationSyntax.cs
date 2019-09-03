using System.Collections.Immutable;
using JurTranspiler.src.syntax_tree.types;

namespace JurTranspiler.compilerSource.nodes {

    public interface IStructOrFunctionDeclarationSyntax {
        bool IsGeneric { get; }
        bool IsPrivate { get; }
        ImmutableArray<TypeParameterSyntax> TypeParameters { get; }
    }

}