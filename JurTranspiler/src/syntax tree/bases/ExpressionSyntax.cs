using System.Collections.Immutable;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.bases {

	public class ExpressionSyntax : SyntaxNode, IExpressionSyntax {

		public virtual bool CanBeAssignedTo => false;
		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }


		protected ExpressionSyntax(ISyntaxNode parent, ParserRuleContext context, ITerminalNode? lineToken = null) : base(parent, context, lineToken) {
		}

	}

}