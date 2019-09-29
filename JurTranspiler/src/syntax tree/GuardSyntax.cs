using System;
using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;
using UtilityLibrary;

namespace JurTranspiler.syntax_tree {

	public class GuardSyntax : SyntaxNode {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
		public IExpressionSyntax? Condition { get; }
		public IExpressionSyntax Expression { get; }
		public bool IsOtherwisePattern { get; }


		public GuardSyntax(ISyntaxNode parent, JurParser.SingleGuardContext context) : base(parent, context) {

			if (context.OTHERWISE() != null) {
				IsOtherwisePattern = true;
				Expression = ToExpression(context.expression(0));
			}
			else {
				Condition = ToExpression(context.expression(0));
				Expression = ToExpression(context.expression(1));
			}

			ImmediateChildren = ImmutableArray<ITreeNode>.Empty
			                                             .AddIfNotNull(Condition)
			                                             .Add(Expression);
		}


		public override string ToJs(Knowledge knowledge) {
			throw new NotImplementedException();
		}

	}

}