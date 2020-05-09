using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.expressions {

	public class PrimitiveValueSyntax : ExpressionSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public string Value { get; }


		public PrimitiveValueSyntax(ISyntaxNode parent, JurParser.PrimitiveValueContext context) : base(parent, context) {

			Value = context.value.Text;
			ImmediateChildren = ImmutableArray.Create<ITreeNode>();

		}


		public override string ToJs(Knowledge knowledge) {
			return Value;
		}

	}

}