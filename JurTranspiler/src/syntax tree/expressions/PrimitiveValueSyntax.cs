using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

	public class PrimitiveValueSyntax : SyntaxNode, IExpressionSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public string Value { get; }
		public bool IsNull => Value == "null";
		public bool IsUndefined => Value == "undefined";


		public PrimitiveValueSyntax(ISyntaxNode parent, JurParser.PrimitiveValueContext context) : base(parent, context) {

			Value = context.value.Text;
			ImmediateChildren = ImmutableArray.Create<ITreeNode>();

		}


		public override string ToJs(Knowledge knowledge) {
			return Value;
		}

	}

}