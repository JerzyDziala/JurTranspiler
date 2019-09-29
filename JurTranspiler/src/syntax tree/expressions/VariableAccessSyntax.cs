using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.expressions {

	public class VariableAccessSyntax : ExpressionSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
		public override bool CanBeAssignedTo => true;
		public string Name { get; }


		public VariableAccessSyntax(ISyntaxNode parent, JurParser.VariableAccessContext context) : base(parent, context) {
			Name = context.ID().GetText();

			ImmediateChildren = ImmutableArray.Create<ITreeNode>();

		}


		public override string ToJs(Knowledge knowledge) {
			return knowledge.GetNewNameFor(this);
		}


		private ImmutableArray<IVariableDeclarationSyntax> GetVisibleDeclarations() {
			return GetVisibleVariablesInScope().Where(x => x.Name == this.Name).ToImmutableArray();
		}


		public IVariableDeclarationSyntax? GetVisibleDeclarationOrNull() {
			var declarations = GetVisibleDeclarations();
			return declarations.Any()
				       ? declarations.First()
				       : null;

		}

	}

}