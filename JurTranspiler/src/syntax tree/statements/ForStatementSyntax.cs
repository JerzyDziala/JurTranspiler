using System;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using System.Resources;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

	public class ForStatementSyntax : ISyntaxNode, IStatementSyntax {

		//INode
		public ISyntaxNode Root { get; }
		public ISyntaxNode Parent { get; }
		public ImmutableList<ISyntaxNode> AllParents { get; }
		public ImmutableList<ITreeNode> ImmediateChildren { get; }
		public ImmutableList<ITreeNode> AllChildren { get; }
		public string File { get; }
		public int Line { get; }
		public int Abstraction { get; }

		public ForLoopType ForLoopType { get; }
		public InitializedVariableDeclarationSyntax Iterator { get; }
		public IExpressionSyntax Condition { get; }
		public IExpressionSyntax Modification { get; }
		public IStatementSyntax Body { get; }


		public ForStatementSyntax(ISyntaxNode parent, JurParser.ForStatementContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;

			if (context.initializedVariableDeclaration() != null) Iterator = new InitializedVariableDeclarationSyntax(this, context.initializedVariableDeclaration());
			Condition = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression(0));
			Modification = context.expression().Length == 2 ? ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression(1)) : null;
			Body = StatementSyntaxFactory.CreateStatementSyntax(this, context.statement());

			if (Iterator != null && Modification != null && Condition != null) ForLoopType = ForLoopType.Classic;
			else if (Iterator != null && Condition != null) ForLoopType = ForLoopType.WhileIterator;
			else if (Condition != null && Modification != null) ForLoopType = ForLoopType.WhileModify;
			else if (Condition != null) ForLoopType = ForLoopType.While;

			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .AddIfNotNull(Iterator)
			                                 .Add(Condition)
			                                 .AddIfNotNull(Modification)
			                                 .Add(Body);

			AllChildren = this.GetAllChildren();

		}


		public string ToJs(Binder binder) {
			switch (ForLoopType) {
				case ForLoopType.Classic:       throw new NotImplementedException("loop code gen not implemented");
				case ForLoopType.While:         return $"while({Condition.ToJs(binder)}) {Body.ToJs(binder)}";
				case ForLoopType.WhileIterator: throw new NotImplementedException("loop code gen not implemented");
				case ForLoopType.WhileModify:   throw new NotImplementedException("loop code gen not implemented");
				default:                        throw new ArgumentOutOfRangeException();
			}
		}

	}


	public enum ForLoopType {

		Classic,
		While,
		WhileIterator,
		WhileModify

	}

}