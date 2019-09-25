using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

	public class InitializerSyntax : SyntaxNode {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public string FieldName { get; }
		public IExpressionSyntax Expression { get; }
		public ConstructorSyntax ParentConstructor => (ConstructorSyntax) Parent!;


		public InitializerSyntax(ISyntaxNode parent, string fieldName, JurParser.ExpressionContext context) : base(parent, context) {
			FieldName = fieldName;
			Expression = ToExpression(context);
			ImmediateChildren = ImmutableArray.Create<ITreeNode>()
			                                  .Add(Expression);
		}

	}

}