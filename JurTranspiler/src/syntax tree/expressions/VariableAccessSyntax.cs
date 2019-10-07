using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.expressions {

	public class VariableAccessSyntax : ExpressionSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
		public override bool CanSyntacticallyBeAssignedTo => true;
		public string Name { get; }


		public VariableAccessSyntax(ISyntaxNode parent, JurParser.VariableAccessContext context) : base(parent, context) {
			Name = context.ID().GetText();

			ImmediateChildren = ImmutableArray.Create<ITreeNode>();

		}


		public override string ToJs(Knowledge knowledge) {
			return knowledge.GetNewNameFor(this);
		}


		public ImmutableArray<IVariableDeclarationSyntax> GetVisibleDefinitionsFor() {
			return GetVisibleVariablesInScope().Where(x => x.Name == this.Name).ToImmutableArray();
		}

		public IVariableDeclarationSyntax? GetVisibleDefinitionOrNull() {
			var declarations = GetVisibleDefinitionsFor();
			return declarations.Any()
				       ? declarations.First()
				       : null;
		}

	}

}