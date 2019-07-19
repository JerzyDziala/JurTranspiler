using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.compilerSource.nodes {

	public class ExpressionStatementSyntax : ISyntaxNode, IStatementSyntax {

		public ISyntaxNode Root { get; }
		public ISyntaxNode Parent { get; }
		public ImmutableList<ISyntaxNode> AllParents { get; }
		public ImmutableList<ITreeNode> ImmediateChildren { get; }
		public ImmutableList<ITreeNode> AllChildren { get; }

		public string File { get; }
		public int Line { get; }
		public int Abstraction { get; }

		public IExpressionSyntax ExpressionSyntax { get; }


		public ExpressionStatementSyntax(ISyntaxNode parent, JurParser.ExpressionStatementContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;

			ExpressionSyntax = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression());
			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .Add(ExpressionSyntax);

			AllChildren = this.GetAllChildren();

		}


		public ExpressionStatementSyntax(ISyntaxNode parent, JurParser.ExpressionContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;

			ExpressionSyntax = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context);
			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .Add(ExpressionSyntax);

			AllChildren = this.GetAllChildren();

		}


                public string ToJs(Knowledge knowledge) {
			return ExpressionSyntax.ToJs(knowledge) +";\n";
		}

	}

}