// Generated from /Users/strator/Documents/GitHub/JurTranspiler/JurTranspiler/src/ANTLR/Jur.g4 by ANTLR 4.7.2
import org.antlr.v4.runtime.tree.ParseTreeVisitor;

/**
 * This interface defines a complete generic visitor for a parse tree produced
 * by {@link JurParser}.
 *
 * @param <T> The return type of the visit operation. Use {@link Void} for
 * operations with no return type.
 */
public interface JurVisitor<T> extends ParseTreeVisitor<T> {
	/**
	 * Visit a parse tree produced by {@link JurParser#program}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitProgram(JurParser.ProgramContext ctx);
	/**
	 * Visit a parse tree produced by {@link JurParser#main}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMain(JurParser.MainContext ctx);
	/**
	 * Visit a parse tree produced by {@link JurParser#abstraction}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAbstraction(JurParser.AbstractionContext ctx);
	/**
	 * Visit a parse tree produced by {@link JurParser#structDeclaration}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitStructDeclaration(JurParser.StructDeclarationContext ctx);
	/**
	 * Visit a parse tree produced by {@link JurParser#inlinedType}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitInlinedType(JurParser.InlinedTypeContext ctx);
	/**
	 * Visit a parse tree produced by {@link JurParser#functionDeclaration}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFunctionDeclaration(JurParser.FunctionDeclarationContext ctx);
	/**
	 * Visit a parse tree produced by {@link JurParser#constraints}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitConstraints(JurParser.ConstraintsContext ctx);
	/**
	 * Visit a parse tree produced by {@link JurParser#constrain}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitConstrain(JurParser.ConstrainContext ctx);
	/**
	 * Visit a parse tree produced by {@link JurParser#uninitializedVarDeclaration}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitUninitializedVarDeclaration(JurParser.UninitializedVarDeclarationContext ctx);
	/**
	 * Visit a parse tree produced by {@link JurParser#initializedVariableDeclaration}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitInitializedVariableDeclaration(JurParser.InitializedVariableDeclarationContext ctx);
	/**
	 * Visit a parse tree produced by {@link JurParser#inferedVariableDeclaration}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitInferedVariableDeclaration(JurParser.InferedVariableDeclarationContext ctx);
	/**
	 * Visit a parse tree produced by the {@code arrayType}
	 * labeled alternative in {@link JurParser#type}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitArrayType(JurParser.ArrayTypeContext ctx);
	/**
	 * Visit a parse tree produced by the {@code typeParameterOrStructType}
	 * labeled alternative in {@link JurParser#type}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitTypeParameterOrStructType(JurParser.TypeParameterOrStructTypeContext ctx);
	/**
	 * Visit a parse tree produced by the {@code functionPointerType}
	 * labeled alternative in {@link JurParser#type}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFunctionPointerType(JurParser.FunctionPointerTypeContext ctx);
	/**
	 * Visit a parse tree produced by the {@code genericStructType}
	 * labeled alternative in {@link JurParser#type}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitGenericStructType(JurParser.GenericStructTypeContext ctx);
	/**
	 * Visit a parse tree produced by the {@code primitiveType}
	 * labeled alternative in {@link JurParser#type}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitPrimitiveType(JurParser.PrimitiveTypeContext ctx);
	/**
	 * Visit a parse tree produced by the {@code blockStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitBlockStatement(JurParser.BlockStatementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code ifStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIfStatement(JurParser.IfStatementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code forStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitForStatement(JurParser.ForStatementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code foreachStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitForeachStatement(JurParser.ForeachStatementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code returnStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitReturnStatement(JurParser.ReturnStatementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code breakStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitBreakStatement(JurParser.BreakStatementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code continueStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitContinueStatement(JurParser.ContinueStatementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code inferedVariableDeclarationStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitInferedVariableDeclarationStatement(JurParser.InferedVariableDeclarationStatementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code initializedVariableDeclarationStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitInitializedVariableDeclarationStatement(JurParser.InitializedVariableDeclarationStatementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code uninitializedVarDeclarationStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitUninitializedVarDeclarationStatement(JurParser.UninitializedVarDeclarationStatementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code assignmentStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAssignmentStatement(JurParser.AssignmentStatementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code expressionStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitExpressionStatement(JurParser.ExpressionStatementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code exitStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitExitStatement(JurParser.ExitStatementContext ctx);
	/**
	 * Visit a parse tree produced by {@link JurParser#block}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitBlock(JurParser.BlockContext ctx);
	/**
	 * Visit a parse tree produced by the {@code variableAccess}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitVariableAccess(JurParser.VariableAccessContext ctx);
	/**
	 * Visit a parse tree produced by the {@code anonymusFunction}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAnonymusFunction(JurParser.AnonymusFunctionContext ctx);
	/**
	 * Visit a parse tree produced by the {@code primitiveValue}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitPrimitiveValue(JurParser.PrimitiveValueContext ctx);
	/**
	 * Visit a parse tree produced by the {@code functionCall}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFunctionCall(JurParser.FunctionCallContext ctx);
	/**
	 * Visit a parse tree produced by the {@code fieldAccess}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFieldAccess(JurParser.FieldAccessContext ctx);
	/**
	 * Visit a parse tree produced by the {@code arrayIndexAccess}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitArrayIndexAccess(JurParser.ArrayIndexAccessContext ctx);
	/**
	 * Visit a parse tree produced by the {@code constructor}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitConstructor(JurParser.ConstructorContext ctx);
	/**
	 * Visit a parse tree produced by the {@code operation}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitOperation(JurParser.OperationContext ctx);
	/**
	 * Visit a parse tree produced by the {@code parExpression}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitParExpression(JurParser.ParExpressionContext ctx);
}