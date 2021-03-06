using System;
using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.declarations {

	public class FieldDeclarationSyntax : SyntaxNode, IVariableDeclarationSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public string Name { get; }
		public ITypeSyntax? Type { get; }
		public bool IsMutable { get; }
		public bool IsPrivate { get; }


		public FieldDeclarationSyntax(StructDefinitionSyntax parent, JurParser.UninitializedVarDeclarationContext context,bool isPrivate = false) : base(parent, context) {

			Name = context.ID().GetText();
			Type = ToType(context.type());
			IsMutable = context.MUTABLE() != null;
			IsPrivate = isPrivate;

			ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(Type);
		}


		public override string ToJs(Knowledge knowledge) {
			throw new NotImplementedException();
		}

	}

}