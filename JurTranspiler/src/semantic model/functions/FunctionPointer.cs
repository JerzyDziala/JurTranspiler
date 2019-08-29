using System.Collections.Immutable;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.semantic_model.functions {

    public class FunctionPointer : Callable {

        public IVariableDeclarationSyntax? declaration { get; }


        public FunctionPointer(IVariableDeclarationSyntax declaration,
                               string name,
                               ImmutableArray<IType> parametersTypes,
                               IType returnType) : base(name, parametersTypes, ImmutableArray<IType>.Empty, returnType) {
            this.declaration = declaration;
        }

    }

}