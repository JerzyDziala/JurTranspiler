// Generated from /Users/strator/Documents/GitHub/JurTranspiler/JurTranspiler/src/ANTLR/Jur.g4 by ANTLR 4.7.2
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link JurParser}.
 */
public interface JurListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link JurParser#program}.
	 * @param ctx the parse tree
	 */
	void enterProgram(JurParser.ProgramContext ctx);
	/**
	 * Exit a parse tree produced by {@link JurParser#program}.
	 * @param ctx the parse tree
	 */
	void exitProgram(JurParser.ProgramContext ctx);
	/**
	 * Enter a parse tree produced by {@link JurParser#main}.
	 * @param ctx the parse tree
	 */
	void enterMain(JurParser.MainContext ctx);
	/**
	 * Exit a parse tree produced by {@link JurParser#main}.
	 * @param ctx the parse tree
	 */
	void exitMain(JurParser.MainContext ctx);
	/**
	 * Enter a parse tree produced by {@link JurParser#abstraction}.
	 * @param ctx the parse tree
	 */
	void enterAbstraction(JurParser.AbstractionContext ctx);
	/**
	 * Exit a parse tree produced by {@link JurParser#abstraction}.
	 * @param ctx the parse tree
	 */
	void exitAbstraction(JurParser.AbstractionContext ctx);
	/**
	 * Enter a parse tree produced by {@link JurParser#structDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterStructDeclaration(JurParser.StructDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link JurParser#structDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitStructDeclaration(JurParser.StructDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link JurParser#inlinedType}.
	 * @param ctx the parse tree
	 */
	void enterInlinedType(JurParser.InlinedTypeContext ctx);
	/**
	 * Exit a parse tree produced by {@link JurParser#inlinedType}.
	 * @param ctx the parse tree
	 */
	void exitInlinedType(JurParser.InlinedTypeContext ctx);
	/**
	 * Enter a parse tree produced by {@link JurParser#functionDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterFunctionDeclaration(JurParser.FunctionDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link JurParser#functionDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitFunctionDeclaration(JurParser.FunctionDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link JurParser#constraints}.
	 * @param ctx the parse tree
	 */
	void enterConstraints(JurParser.ConstraintsContext ctx);
	/**
	 * Exit a parse tree produced by {@link JurParser#constraints}.
	 * @param ctx the parse tree
	 */
	void exitConstraints(JurParser.ConstraintsContext ctx);
	/**
	 * Enter a parse tree produced by {@link JurParser#constrain}.
	 * @param ctx the parse tree
	 */
	void enterConstrain(JurParser.ConstrainContext ctx);
	/**
	 * Exit a parse tree produced by {@link JurParser#constrain}.
	 * @param ctx the parse tree
	 */
	void exitConstrain(JurParser.ConstrainContext ctx);
	/**
	 * Enter a parse tree produced by {@link JurParser#uninitializedVarDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterUninitializedVarDeclaration(JurParser.UninitializedVarDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link JurParser#uninitializedVarDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitUninitializedVarDeclaration(JurParser.UninitializedVarDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link JurParser#initializedVariableDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterInitializedVariableDeclaration(JurParser.InitializedVariableDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link JurParser#initializedVariableDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitInitializedVariableDeclaration(JurParser.InitializedVariableDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link JurParser#inferedVariableDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterInferedVariableDeclaration(JurParser.InferedVariableDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link JurParser#inferedVariableDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitInferedVariableDeclaration(JurParser.InferedVariableDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by the {@code arrayType}
	 * labeled alternative in {@link JurParser#type}.
	 * @param ctx the parse tree
	 */
	void enterArrayType(JurParser.ArrayTypeContext ctx);
	/**
	 * Exit a parse tree produced by the {@code arrayType}
	 * labeled alternative in {@link JurParser#type}.
	 * @param ctx the parse tree
	 */
	void exitArrayType(JurParser.ArrayTypeContext ctx);
	/**
	 * Enter a parse tree produced by the {@code typeParameterOrStructType}
	 * labeled alternative in {@link JurParser#type}.
	 * @param ctx the parse tree
	 */
	void enterTypeParameterOrStructType(JurParser.TypeParameterOrStructTypeContext ctx);
	/**
	 * Exit a parse tree produced by the {@code typeParameterOrStructType}
	 * labeled alternative in {@link JurParser#type}.
	 * @param ctx the parse tree
	 */
	void exitTypeParameterOrStructType(JurParser.TypeParameterOrStructTypeContext ctx);
	/**
	 * Enter a parse tree produced by the {@code functionPointerType}
	 * labeled alternative in {@link JurParser#type}.
	 * @param ctx the parse tree
	 */
	void enterFunctionPointerType(JurParser.FunctionPointerTypeContext ctx);
	/**
	 * Exit a parse tree produced by the {@code functionPointerType}
	 * labeled alternative in {@link JurParser#type}.
	 * @param ctx the parse tree
	 */
	void exitFunctionPointerType(JurParser.FunctionPointerTypeContext ctx);
	/**
	 * Enter a parse tree produced by the {@code genericStructType}
	 * labeled alternative in {@link JurParser#type}.
	 * @param ctx the parse tree
	 */
	void enterGenericStructType(JurParser.GenericStructTypeContext ctx);
	/**
	 * Exit a parse tree produced by the {@code genericStructType}
	 * labeled alternative in {@link JurParser#type}.
	 * @param ctx the parse tree
	 */
	void exitGenericStructType(JurParser.GenericStructTypeContext ctx);
	/**
	 * Enter a parse tree produced by the {@code primitiveType}
	 * labeled alternative in {@link JurParser#type}.
	 * @param ctx the parse tree
	 */
	void enterPrimitiveType(JurParser.PrimitiveTypeContext ctx);
	/**
	 * Exit a parse tree produced by the {@code primitiveType}
	 * labeled alternative in {@link JurParser#type}.
	 * @param ctx the parse tree
	 */
	void exitPrimitiveType(JurParser.PrimitiveTypeContext ctx);
	/**
	 * Enter a parse tree produced by the {@code blockStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterBlockStatement(JurParser.BlockStatementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code blockStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitBlockStatement(JurParser.BlockStatementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ifStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterIfStatement(JurParser.IfStatementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ifStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitIfStatement(JurParser.IfStatementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code forStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterForStatement(JurParser.ForStatementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code forStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitForStatement(JurParser.ForStatementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code foreachStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterForeachStatement(JurParser.ForeachStatementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code foreachStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitForeachStatement(JurParser.ForeachStatementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code returnStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterReturnStatement(JurParser.ReturnStatementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code returnStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitReturnStatement(JurParser.ReturnStatementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code breakStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterBreakStatement(JurParser.BreakStatementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code breakStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitBreakStatement(JurParser.BreakStatementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code continueStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterContinueStatement(JurParser.ContinueStatementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code continueStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitContinueStatement(JurParser.ContinueStatementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code inferedVariableDeclarationStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterInferedVariableDeclarationStatement(JurParser.InferedVariableDeclarationStatementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code inferedVariableDeclarationStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitInferedVariableDeclarationStatement(JurParser.InferedVariableDeclarationStatementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code initializedVariableDeclarationStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterInitializedVariableDeclarationStatement(JurParser.InitializedVariableDeclarationStatementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code initializedVariableDeclarationStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitInitializedVariableDeclarationStatement(JurParser.InitializedVariableDeclarationStatementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code uninitializedVarDeclarationStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterUninitializedVarDeclarationStatement(JurParser.UninitializedVarDeclarationStatementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code uninitializedVarDeclarationStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitUninitializedVarDeclarationStatement(JurParser.UninitializedVarDeclarationStatementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code assignmentStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterAssignmentStatement(JurParser.AssignmentStatementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code assignmentStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitAssignmentStatement(JurParser.AssignmentStatementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code expressionStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterExpressionStatement(JurParser.ExpressionStatementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code expressionStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitExpressionStatement(JurParser.ExpressionStatementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code exitStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterExitStatement(JurParser.ExitStatementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code exitStatement}
	 * labeled alternative in {@link JurParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitExitStatement(JurParser.ExitStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link JurParser#block}.
	 * @param ctx the parse tree
	 */
	void enterBlock(JurParser.BlockContext ctx);
	/**
	 * Exit a parse tree produced by {@link JurParser#block}.
	 * @param ctx the parse tree
	 */
	void exitBlock(JurParser.BlockContext ctx);
	/**
	 * Enter a parse tree produced by the {@code variableAccess}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterVariableAccess(JurParser.VariableAccessContext ctx);
	/**
	 * Exit a parse tree produced by the {@code variableAccess}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitVariableAccess(JurParser.VariableAccessContext ctx);
	/**
	 * Enter a parse tree produced by the {@code anonymusFunction}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterAnonymusFunction(JurParser.AnonymusFunctionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code anonymusFunction}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitAnonymusFunction(JurParser.AnonymusFunctionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code primitiveValue}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterPrimitiveValue(JurParser.PrimitiveValueContext ctx);
	/**
	 * Exit a parse tree produced by the {@code primitiveValue}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitPrimitiveValue(JurParser.PrimitiveValueContext ctx);
	/**
	 * Enter a parse tree produced by the {@code functionCall}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterFunctionCall(JurParser.FunctionCallContext ctx);
	/**
	 * Exit a parse tree produced by the {@code functionCall}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitFunctionCall(JurParser.FunctionCallContext ctx);
	/**
	 * Enter a parse tree produced by the {@code fieldAccess}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterFieldAccess(JurParser.FieldAccessContext ctx);
	/**
	 * Exit a parse tree produced by the {@code fieldAccess}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitFieldAccess(JurParser.FieldAccessContext ctx);
	/**
	 * Enter a parse tree produced by the {@code arrayIndexAccess}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterArrayIndexAccess(JurParser.ArrayIndexAccessContext ctx);
	/**
	 * Exit a parse tree produced by the {@code arrayIndexAccess}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitArrayIndexAccess(JurParser.ArrayIndexAccessContext ctx);
	/**
	 * Enter a parse tree produced by the {@code constructor}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterConstructor(JurParser.ConstructorContext ctx);
	/**
	 * Exit a parse tree produced by the {@code constructor}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitConstructor(JurParser.ConstructorContext ctx);
	/**
	 * Enter a parse tree produced by the {@code operation}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterOperation(JurParser.OperationContext ctx);
	/**
	 * Exit a parse tree produced by the {@code operation}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitOperation(JurParser.OperationContext ctx);
	/**
	 * Enter a parse tree produced by the {@code parExpression}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterParExpression(JurParser.ParExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code parExpression}
	 * labeled alternative in {@link JurParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitParExpression(JurParser.ParExpressionContext ctx);
}