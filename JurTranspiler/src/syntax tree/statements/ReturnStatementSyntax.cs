using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

	public class ReturnStatementSyntax : ISyntaxNode, IStatementSyntax {

		//INode
		public ISyntaxNode Root { get; }
		public ISyntaxNode Parent { get; }
		public ImmutableList<ISyntaxNode> AllParents { get; }
		public ImmutableList<ITreeNode> ImmediateChildren { get; }
		public ImmutableList<ITreeNode> AllChildren { get; }

		public string File { get; }
		public int Line { get; }
		public int Abstraction { get; }

		public IExpressionSyntax ReturnValue { get; }
		public bool IsVoid { get; }


		public ReturnStatementSyntax(ISyntaxNode parent, JurParser.ReturnStatementContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;

			if (context.expression() != null) {
				ReturnValue = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression());
			}

			IsVoid = ReturnValue == null;

			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .AddIfNotNull(ReturnValue);

			AllChildren = this.GetAllChildren();

		}


                public string ToJs(Knowledge knowledge) {
			return $"return{(IsVoid ? "" : $" {ReturnValue.ToJs(knowledge)}")};\n";
		}

	}

}