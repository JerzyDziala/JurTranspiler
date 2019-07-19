using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.nodes {

	public class ArrayIndexAccessSyntax : IExpressionSyntax, ISyntaxNode {

		//INode
		public override ISyntaxNode Root { get; }
		public override ISyntaxNode Parent { get; }
		public override ImmutableList<ISyntaxNode> AllParents { get; }
		public override ImmutableList<ITreeNode> ImmediateChildren { get; }
		public override ImmutableList<ITreeNode> AllChildren { get; }

		public override string File { get; }
		public override int Line { get; }

		public override int Abstraction { get; }

		public IExpressionSyntax Array { get; }
		public IExpressionSyntax Index { get; }


		public ArrayIndexAccessSyntax(ISyntaxNode parent, JurParser.ArrayIndexAccessContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;

			Array = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression(0));
			Index = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression(1));

			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .Add(Array)
			                                 .Add(Index);

			AllChildren = this.GetAllChildren();

		}




                public override string ToJs(Knowledge knowledge) {
			return $"{Array.ToJs(knowledge)}[{Index.ToJs(knowledge)}]";
		}

	}

}