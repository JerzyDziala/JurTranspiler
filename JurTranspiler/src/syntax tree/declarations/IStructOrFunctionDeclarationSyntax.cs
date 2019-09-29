using System.Collections.Immutable;
using JurTranspiler.syntax_tree.types;

namespace JurTranspiler.syntax_tree.declarations {

    public interface IStructOrFunctionDeclarationSyntax {
        bool IsGeneric { get; }
        bool IsPrivate { get; }
        ImmutableArray<TypeParameterSyntax> TypeParameters { get; }
    }

}