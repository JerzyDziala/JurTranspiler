using System;

namespace JurTranspiler.compilerSource.nodes {

	public static class StatementSyntaxFactory {

		public static IStatementSyntax CreateStatementSyntax(ISyntaxNode parent, JurParser.StatementContext context) {
			if (context is JurParser.BlockStatementContext blockContext) return new BlockStatement(parent, blockContext);
			if(context is JurParser.IfStatementContext ifContext) return new IfStatementSyntax(parent,ifContext);
			if(context is JurParser.ForStatementContext forContext) return new ForStatementSyntax(parent,forContext);
			if(context is JurParser.ForeachStatementContext foreachContext) return new ForEachStatementSyntax(parent,foreachContext);
			if(context is JurParser.ReturnStatementContext returnContext) return new ReturnStatementSyntax(parent,returnContext);
			if(context is JurParser.BreakStatementContext breakContext) return new BreakStatementSyntax(parent, breakContext);
			if(context is JurParser.ContinueStatementContext continueContext) return new ContinueStatementSyntax(parent, continueContext);
			if(context is JurParser.InferedVariableDeclarationStatementContext inferredContext) return new InferredVariableDeclarationSyntax(parent,inferredContext.inferedVariableDeclaration());
			if(context is JurParser.InitializedVariableDeclarationStatementContext initializedContext) return new InitializedVariableDeclarationSyntax(parent,initializedContext.initializedVariableDeclaration());
			if(context is JurParser.UninitializedVarDeclarationStatementContext uninitializedContext) return new UninitializedVariableDeclarationSyntax(parent,uninitializedContext.uninitializedVarDeclaration(),false);
			if(context is JurParser.AssignmentStatementContext assignmentContext) return new AssignmentStatementSyntax(parent,assignmentContext);
			if (context is JurParser.ExpressionStatementContext expressionContext) return new ExpressionStatementSyntax(parent, expressionContext);
            throw new Exception("WTF");
		}

	}

}