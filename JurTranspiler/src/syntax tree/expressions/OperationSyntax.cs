using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.expressions {

	public class OperationSyntax : ExpressionSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public string Operator { get; }
		public IExpressionSyntax Left { get; }
		public IExpressionSyntax Right { get; }


		public OperationSyntax(ISyntaxNode parent, JurParser.OperationContext context) : base(parent, context) {

			var op = context.@operator?.Text ?? context.LOGICAL_AND()?.GetText() ?? context.OR()?.GetText();
			Operator = op!;
			Left = ToExpression(context.expression(0));
			Right = ToExpression(context.expression(1));
			ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(Left).Add(Right);

		}


		public override string ToJs(Knowledge knowledge) {
			return $"{Left.ToJs(knowledge)} {(Operator == "==" ? "===" : Operator)} {Right.ToJs(knowledge)}";
		}

	}

}