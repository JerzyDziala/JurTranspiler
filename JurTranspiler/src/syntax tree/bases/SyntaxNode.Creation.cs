using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;

namespace JurTranspiler.syntax_tree.bases {

    public abstract partial class SyntaxNode {

        protected ImmutableArray<IExpressionSyntax> ToExpressions(JurParser.ExpressionContext[] contexts) {
            return contexts.Select(x => ExpressionSyntaxFactory.Create(this, x)).ToImmutableArray();
        }


        protected ImmutableArray<ITypeSyntax> ToTypes(JurParser.TypeContext[] contexts) {
            return contexts.Select(x => TypeSyntaxFactory.Create(this, x)).ToImmutableArray();
        }


        protected ImmutableArray<IStatementSyntax> ToStatements(JurParser.StatementContext[] contexts) {
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


        protected ImmutableArray<FunctionDefinitionSyntax> ToFunctionDefinitions(JurParser.FunctionDeclarationContext[] contexts) {
            return contexts.Select(x => new FunctionDefinitionSyntax(this, x)).ToImmutableArray();
        }


        protected ImmutableArray<UninitializedVariableDeclarationSyntax> ToUninitializedVariablesDefinitions(JurParser.UninitializedVarDeclarationContext[] contexts, bool areParameters) {
            return contexts.Select(x => new UninitializedVariableDeclarationSyntax(this, x, areParameters)).ToImmutableArray();
        }


        protected ImmutableArray<StructDefinitionSyntax> ToStructDefinitions(JurParser.StructDeclarationContext[] contexts) {
            return contexts.Select(x => new StructDefinitionSyntax(this, x)).ToImmutableArray();
        }


        protected ImmutableArray<FieldDeclarationSyntax> ToFields(JurParser.UninitializedVarDeclarationContext[] contexts) {
            return contexts.Select(x => new FieldDeclarationSyntax((StructDefinitionSyntax) this, x)).ToImmutableArray();
        }


        protected ImmutableArray<AbstractionSyntax> ToAbstractions(JurParser.AbstractionContext[] contexts) {
            return contexts.Select(x => new AbstractionSyntax((ProgramFileSyntax) this, x)).ToImmutableArray();
        }


        protected ImmutableArray<MainSyntax> ToMains(JurParser.MainContext[] contexts) {
            return contexts.Select(x => new MainSyntax(this, x)).ToImmutableArray();
        }
    }

}