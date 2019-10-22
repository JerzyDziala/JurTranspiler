using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.expressions {

	public class IncrementationOrDecrementationExpressionSyntax : ExpressionSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public string Operator { get; }
		public IExpressionSyntax Expression { get; }


		public IncrementationOrDecrementationExpressionSyntax(ISyntaxNode parent, JurParser.IncrementOrDecrementContext context) : base(parent, context) {
			Operator = context.@operator.Text;
			Expression = ToExpression(context.expression());
			ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(Expression);
		}


		public override string ToJs(Knowledge knowledge) {
			return $"{Expression.ToJs(knowledge)}{Operator}";
		}

	}

}