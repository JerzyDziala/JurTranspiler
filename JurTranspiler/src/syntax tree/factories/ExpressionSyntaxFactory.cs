using System;
using JurTranspiler.syntax_tree.expressions;
using JurTranspiler.syntax_tree.Interfaces;
using static JurParser;

namespace JurTranspiler.syntax_tree.factories {

	public static class ExpressionSyntaxFactory {

		public static IExpressionSyntax Create(ISyntaxNode parent, ExpressionContext context)
			=> context switch {
				//there is an issue with jetbrains rider when it complains about not finding a common type
				//even when the compiler says it's ok, to get rid of the warning i cast first arm to IExpressionSyntax'
				   VariableAccessContext varContext => (IExpressionSyntax)new VariableAccessSyntax(parent, varContext) ,
				   PrimitiveValueContext primitiveContext => new PrimitiveValueSyntax(parent, primitiveContext),
				   AnonymusFunctionContext anonymousFunctionContext => new AnonymousFunctionSyntax(parent, anonymousFunctionContext),
				   FunctionCallContext callContext => new FunctionCallSyntax(parent, callContext),
				   FieldAccessContext fieldContext => new FieldAccessSyntax(parent, fieldContext),
				   ConstructorContext constructorContext => new ConstructorSyntax(parent, constructorContext),
				   ArrayIndexAccessContext arrayIndexAccessContext => new ArrayIndexAccessSyntax(parent, arrayIndexAccessContext),
				   ParExpressionContext parExpressionContext => new ParenthesisSyntax(parent, parExpressionContext),
				   OperationContext operationContext => new OperationSyntax(parent, operationContext),
				   DefaultValueContext defaultContext => new DefaultTypeValueSyntax(parent, defaultContext),
				   TypeExpressionContext typeExpressionContext => new TypeExpressionSyntax(parent, typeExpressionContext),
				   NegationContext negationContext => new NegationExpressionSyntax(parent, negationContext),
				   IncrementOrDecrementContext incrementOrDecrementContext => new IncrementationOrDecrementationExpression(parent, incrementOrDecrementContext),
				   ArithmeticNegationContext arithmeticNegationContext => new ArithmeticNegationExpressionSyntax(parent, arithmeticNegationContext),
				   GuardContext guardContext => new GuardExpressionSyntax(parent, guardContext),
				   _ => throw new Exception("You forgot to add new expression here")
				   };
	}

}