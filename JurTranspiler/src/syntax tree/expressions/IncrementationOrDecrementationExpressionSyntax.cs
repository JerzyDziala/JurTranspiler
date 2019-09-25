using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

	public class IncrementationOrDecrementationExpression : SyntaxNode, IExpressionSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public string Operator { get; }
		public IExpressionSyntax Expression { get; }


		public IncrementationOrDecrementationExpression(ISyntaxNode parent, JurParser.IncrementOrDecrementContext context) : base(parent, context) {
			Operator = context.@operator.Text;
			Expression = ToExpression(context.expression());
			ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(Expression);
		}


		public override string ToJs(Knowledge knowledge) {
			return $"{Expression.ToJs(knowledge)}{Operator}";
		}

	}

}