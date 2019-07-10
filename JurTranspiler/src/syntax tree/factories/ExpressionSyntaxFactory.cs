using System;

namespace JurTranspiler.compilerSource.nodes {

    public static class ExpressionSyntaxFactory {

        public static IExpressionSyntax CreateExpressionSyntax(ISyntaxNode parent, JurParser.ExpressionContext context) {
            if (context is JurParser.VariableAccessContext varContext) return new VariableAccessSyntax(parent, varContext);
            if (context is JurParser.PrimitiveValueContext primitiveContext) return new PrimitiveValueSyntax(parent, primitiveContext);
            if (context is JurParser.AnonymusFunctionContext anonymousFunctionContext) return new AnonymousFunctionSyntax(parent, anonymousFunctionContext);
            if (context is JurParser.FunctionCallContext callContext) return new FunctionCallSyntax(parent, callContext);
            if (context is JurParser.FieldAccessContext fieldContext) return new FieldAccessSyntax(parent, fieldContext);
            if (context is JurParser.ConstructorContext constructorContext) return new ConstructorSyntax(parent, constructorContext);
            if (context is JurParser.ArrayIndexAccessContext arrayIndexAccessContext) return new ArrayIndexAccessSyntax(parent, arrayIndexAccessContext);
            if (context is JurParser.ParExpressionContext parExpressionContext) return new ParenthesisSyntax(parent, parExpressionContext);
            if (context is JurParser.OperationContext operationContext) return new OperationSyntax(parent, operationContext);

            throw new Exception("WTF");
        }

    }

}