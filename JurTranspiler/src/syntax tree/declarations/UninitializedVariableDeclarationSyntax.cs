using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.nodes {

	public class UninitializedVariableDeclarationSyntax : ISyntaxNode, IStatementSyntax, IVariableDeclarationSyntax {

		//INode
		public ISyntaxNode Root { get; }
		public ISyntaxNode Parent { get; }
		public ImmutableList<ISyntaxNode> AllParents { get; }
		public ImmutableList<ITreeNode> ImmediateChildren { get; }
		public ImmutableList<ITreeNode> AllChildren { get; }

		public string File { get; }
		public int Line { get; }
		public int Abstraction { get; }
		public string Name { get; }
		public ITypeSyntax Type { get; }
		public bool IsParameter { get; }


		public UninitializedVariableDeclarationSyntax(ISyntaxNode parent, JurParser.UninitializedVarDeclarationContext context, bool isParameter) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;
			Name = context.ID().GetText();
			IsParameter = isParameter;
			Type = TypeSyntaxFactory.Create(this, context.type());

			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .Add(Type);
			AllChildren = this.GetAllChildren();
		}




		public string ToJs(Binder binder) {
			return $"let {Name}";
		}

	}

}