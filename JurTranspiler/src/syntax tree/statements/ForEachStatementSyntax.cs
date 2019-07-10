using System;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.compilerSource.nodes {

	public class ForEachStatementSyntax : ISyntaxNode, IStatementSyntax {

		//INode
		public ISyntaxNode Root { get; }
		public ISyntaxNode Parent { get; }
		public ImmutableList<ISyntaxNode> AllParents { get; }
		public ImmutableList<ITreeNode> ImmediateChildren { get; }
		public ImmutableList<ITreeNode> AllChildren { get; }

		public string File { get; }
		public int Line { get; }
		public int Abstraction { get; }

		public UninitializedVariableDeclarationSyntax IteratorVariable { get; }
		public IExpressionSyntax CollectionToIterate { get; }
		public IStatementSyntax Body { get; }


		public ForEachStatementSyntax(ISyntaxNode parent, JurParser.ForeachStatementContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;

			CollectionToIterate = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression());
			IteratorVariable = new UninitializedVariableDeclarationSyntax(this, context.uninitializedVarDeclaration(), false);
			Body = StatementSyntaxFactory.CreateStatementSyntax(this, context.statement());

			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .Add(IteratorVariable)
			                                 .Add(CollectionToIterate)
			                                 .Add(Body);

			AllChildren = this.GetAllChildren();

		}


		public string ToJs(Binder binder) {
//            return $"for (var i{binder.GetUniqueId()} = 0; {binder.GetSizeFunctionFor()}"
			throw new NotImplementedException();
		}

	}

}