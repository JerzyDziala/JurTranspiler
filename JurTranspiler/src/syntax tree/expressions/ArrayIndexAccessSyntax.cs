using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.expressions {

	public class ArrayIndexAccessSyntax : ExpressionSyntax {

		//INode
		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public IExpressionSyntax Array { get; }
		public IExpressionSyntax Index { get; }
		public override bool CanBeAssignedTo => true;


		public ArrayIndexAccessSyntax(ISyntaxNode parent, JurParser.ArrayIndexAccessContext context) : base(parent, context) {

			Array = ToExpression(context.expression(0));
			Index = ToExpression(context.expression(1));

			ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(Array).Add(Index);

		}


		public override string ToJs(Knowledge knowledge) {
			return $"{Array.ToJs(knowledge)}[{Index.ToJs(knowledge)}]";
		}

	}

}