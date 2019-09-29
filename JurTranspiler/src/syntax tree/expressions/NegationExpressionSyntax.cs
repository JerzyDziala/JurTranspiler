using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.expressions {

	public class NegationExpressionSyntax : ExpressionSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public IExpressionSyntax Expression { get; }


		public NegationExpressionSyntax(ISyntaxNode parent, JurParser.NegationContext context) : base(parent, context) {

			Expression = ToExpression(context.expression());

			ImmediateChildren = ImmutableArray.Create<ITreeNode>()
			                                  .Add(Expression);
		}


		public override string ToJs(Knowledge knowledge) => "!" + Expression.ToJs(knowledge);

	}

}