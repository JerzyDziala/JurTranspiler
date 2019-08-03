using System;
using System.Linq;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.src.syntax_tree.types;

namespace JurTranspiler.compilerSource.nodes {

    public static class TypeSyntaxFactory {

        public static ITypeSyntax Create(ISyntaxNode parent, JurParser.TypeContext context) {
            if (context is JurParser.PrimitiveTypeContext primitiveTypeContext) return new PrimitiveTypeSyntax(parent, primitiveTypeContext);
            if (context is JurParser.ArrayTypeContext arrayTypeContext) return new ArrayTypeSyntax(parent, arrayTypeContext);
            if (context is JurParser.FunctionPointerTypeContext functionPointerTypeContext) return new FunctionPointerTypeSyntax(parent, functionPointerTypeContext);
            if (context is JurParser.GenericStructTypeContext genericStructTypeContext) return new StructTypeSyntax(parent, genericStructTypeContext);
            if (context is JurParser.TypeParameterOrStructTypeContext ambiguous) {

                var parentDeclaration = parent.AllParents
                                              .Add(parent)
                                              .OfType<IStructOrFunctionDeclarationSyntax>()
                                              .FirstOrDefault();

                //we are in main.
                if (parentDeclaration == null) return new StructTypeSyntax(parent, ambiguous);

                //we are in generic struct or function declaration and we were declared in this scope
                var hasTypeParameterWithMyName = parentDeclaration.TypeParameters.Any(x => x.Name == ambiguous.ID().GetText());
                if (hasTypeParameterWithMyName) return new TypeParameterSyntax(parent, ambiguous, parentDeclaration);

                //we were not declared as typeParameter
                else return new StructTypeSyntax(parent, ambiguous);

            }

            throw new Exception("WTF?");
        }

    }

}