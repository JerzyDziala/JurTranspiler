using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.nodes {

	public class FieldDeclarationSyntax : ISyntaxNode, IVariableDeclarationSyntax {

		public ISyntaxNode Root { get; }
		public ISyntaxNode Parent { get; }
		public ImmutableList<ISyntaxNode> AllParents { get; }
		public ImmutableList<ITreeNode> ImmediateChildren { get; }
		public ImmutableList<ITreeNode> AllChildren { get; }
		public int Abstraction { get; }
		public string File { get; }
		public int Line { get; }

		public string Name { get; }
		public ITypeSyntax Type { get; }


		public FieldDeclarationSyntax(StructDefinitionSyntax parent, JurParser.UninitializedVarDeclarationContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;
			Name = context.ID().GetText();
			Type = TypeSyntaxFactory.Create(this, context.type());

			ImmediateChildren = ImmutableList.Create<ITreeNode>().Add(Type);
			AllChildren = this.GetAllChildren();

		}




                public string ToJs(Knowledge knowledge) {
			throw new NotImplementedException();
		}

	}

}