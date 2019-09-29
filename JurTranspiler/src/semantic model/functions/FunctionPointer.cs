using System.Collections.Immutable;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.semantic_model.functions {

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