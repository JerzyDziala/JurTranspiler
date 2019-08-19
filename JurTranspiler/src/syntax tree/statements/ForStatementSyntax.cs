using System;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using UtilityLibrary;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

	public class ForStatementSyntax : SyntaxNode, IStatementSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public ForLoopType ForLoopType { get; }
		public IVariableDeclarationSyntax? Iterator { get; }
		public IExpressionSyntax Condition { get; }
		public IExpressionSyntax? Modification { get; }
		public IStatementSyntax Body { get; }


		public ForStatementSyntax(ISyntaxNode parent, JurParser.ForStatementContext context) : base(parent, context) {

			Iterator = context.initializedVariableDeclaration() != null ? new InitializedVariableDeclarationSyntax(this, context.initializedVariableDeclaration())
			           : context.inferedVariableDeclaration() != null   ? new InferredVariableDeclarationSyntax(this, context.inferedVariableDeclaration())
			                                                              : (IVariableDeclarationSyntax?) null;

			Condition = ToExpression(context.expression(0));
			Modification = context.expression().Length == 2 ? ToExpression(context.expression(1)) : null;
			Body = StatementSyntaxFactory.Create(this, context.statement());

			ForLoopType = Iterator != null && Modification != null ? ForLoopType.Classic
			              : Iterator != null                       ? ForLoopType.WhileIterator
			              : Modification != null                   ? ForLoopType.WhileModify
			                                                         : ForLoopType.While;

			ImmediateChildren = ImmutableArray.Create<ITreeNode>()
			                                  .AddIfNotNull(Iterator)
			                                  .Add(Condition)
			                                  .AddIfNotNull(Modification)
			                                  .Add(Body);
		}


		public override string ToJs(Knowledge knowledge) => ForLoopType switch {
			                                                    ForLoopType.Classic => $"for({Iterator!.ToJs(knowledge)} {Condition!.ToJs(knowledge)}; {Modification!.ToJs(knowledge)}) {Body!.ToJs(knowledge)}",
			                                                    ForLoopType.While => $"while({Condition.ToJs(knowledge)}) {Body!.ToJs(knowledge)}",
			                                                    ForLoopType.WhileIterator => $"for({Iterator!.ToJs(knowledge)} {Condition.ToJs(knowledge)};) {Body!.ToJs(knowledge)}",
			                                                    ForLoopType.WhileModify => $"for(;{Condition.ToJs(knowledge)};{Modification!.ToJs(knowledge)};) {Body!.ToJs(knowledge)}",
			                                                    _ => throw new ArgumentOutOfRangeException()
			                                                    };

	}

}