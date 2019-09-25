using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

	public class ArithmeticNegationExpressionSyntax : SyntaxNode, IExpressionSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public IExpressionSyntax Expression { get; }


		public ArithmeticNegationExpressionSyntax(ISyntaxNode parent, JurParser.ArithmeticNegationContext context) : base(parent, context) {

			Expression = ToExpression(context.expression());

			ImmediateChildren = ImmutableArray.Create<ITreeNode>()
			                                  .Add(Expression);
		}


		public override string ToJs(Knowledge knowledge) => "-" + Expression.ToJs(knowledge);

	}

}