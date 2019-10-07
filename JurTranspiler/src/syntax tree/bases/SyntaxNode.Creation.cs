using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.syntax_tree.declarations;
using JurTranspiler.syntax_tree.factories;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.bases {

	public abstract partial class SyntaxNode {

		protected ImmutableArray<IExpressionSyntax> ToExpressions(IEnumerable<JurParser.ExpressionContext> contexts) {
			return contexts.Select(x => ExpressionSyntaxFactory.Create(this, x)).ToImmutableArray();
		}


		protected ImmutableArray<ITypeSyntax> ToTypes(IEnumerable<JurParser.TypeContext> contexts) {
			return contexts.Select(x => TypeSyntaxFactory.Create(this, x)).ToImmutableArray();
		}


		protected ImmutableArray<IStatementSyntax> ToStatements(IEnumerable<JurParser.StatementContext> contexts) {
			return contexts.Select(x => StatementSyntaxFactory.Create(this, x)).ToImmutableArray();
		}


		protected ITypeSyntax ToType(JurParser.TypeContext context) {
			return TypeSyntaxFactory.Create(this, context);
		}


		protected IExpressionSyntax ToExpression(JurParser.ExpressionContext context) {
			return ExpressionSyntaxFactory.Create(this, context);
		}


		protected IStatementSyntax ToStatement(JurParser.StatementContext context) {
			return StatementSyntaxFactory.Create(this, context);
		}


		protected ImmutableArray<FunctionDefinitionSyntax> ToFunctionDefinitions(IEnumerable<JurParser.FunctionDeclarationContext> contexts) {
			return contexts.Select(x => new FunctionDefinitionSyntax(this, x)).ToImmutableArray();
		}


		protected ImmutableArray<UninitializedVariableDeclarationSyntax> ToUninitializedVariablesDefinitions(IEnumerable<JurParser.UninitializedVarDeclarationContext> contexts, UninitializedVariableType uninitializedVariableType) {
			return contexts.Select(x => new UninitializedVariableDeclarationSyntax(this, x, uninitializedVariableType)).ToImmutableArray();
		}


		protected ImmutableArray<StructDefinitionSyntax> ToStructDefinitions(IEnumerable<JurParser.StructDeclarationContext> contexts) {
			return contexts.Select(x => new StructDefinitionSyntax(this, x)).ToImmutableArray();
		}


		protected ImmutableArray<FieldDeclarationSyntax> ToFields(IEnumerable<JurParser.UninitializedVarDeclarationContext> contexts) {
			return contexts.Select(x => new FieldDeclarationSyntax((StructDefinitionSyntax) this, x)).ToImmutableArray();
		}


		protected ImmutableArray<AbstractionSyntax> ToAbstractions(IEnumerable<JurParser.AbstractionContext> contexts) {
			return contexts.Select(x => new AbstractionSyntax((ProgramFileSyntax) this, x)).ToImmutableArray();
		}


		protected ImmutableArray<MainSyntax> ToMains(IEnumerable<JurParser.MainContext> contexts) {
			return contexts.Select(x => new MainSyntax(this, x)).ToImmutableArray();
		}

	}

}