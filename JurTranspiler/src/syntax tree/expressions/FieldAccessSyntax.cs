using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.factories;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.expressions {

	public class FieldAccessSyntax : ExpressionSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public IExpressionSyntax Owner { get; }
		public string Name { get; }
		public override bool CanSyntacticallyBeAssignedTo => true;


		public FieldAccessSyntax(ISyntaxNode parent, JurParser.FieldAccessContext context) : base(parent, context) {

			Owner = ExpressionSyntaxFactory.Create(this, context.expression());
			Name = context.ID().GetText();
			ImmediateChildren = ImmutableArray.Create<ITreeNode>()
			                                  .Add(Owner);

		}


		public override string ToJs(Knowledge knowledge) {
			return $"{Owner.ToJs(knowledge)}.{Name}";
		}

	}

}