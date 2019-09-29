using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;
using UtilityLibrary;

namespace JurTranspiler.syntax_tree.expressions {

	public class GuardExpressionSyntax : ExpressionSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public ImmutableArray<GuardSyntax> Guards { get; }


		public GuardExpressionSyntax(ISyntaxNode parent, JurParser.GuardContext context) : base(parent, context) {

			Guards = context.singleGuard()
			                .Select(guard => new GuardSyntax(this, guard))
			                .ToImmutableArray();

			ImmediateChildren = ImmutableArray.Create<ITreeNode>()
			                                  .AddRange(Guards);

		}


		public override string ToJs(Knowledge knowledge) {

			var output = Guards.Select((guard, i) => {
				var expressionString = guard.Expression.ToJs(knowledge);
				return !guard.IsOtherwisePattern
					       ? $"{guard.Condition!.ToJs(knowledge)} ? {expressionString} : "
					       : $"{expressionString}";
			}).Glue();
			return output;
		}



	}

}