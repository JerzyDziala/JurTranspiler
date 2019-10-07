using System;
using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.declarations {
	
	public enum UninitializedVariableType {
		Local,
		Parameter,
		Field
	}

	public class UninitializedVariableDeclarationSyntax : SyntaxNode, IStatementSyntax, IVariableDeclarationSyntax {

		
		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public string Name { get; }
		public ITypeSyntax? Type { get; }
		public UninitializedVariableType UninitializedVariableType { get; }
		public bool IsMutable { get; }


		public UninitializedVariableDeclarationSyntax(ISyntaxNode parent, JurParser.UninitializedVarDeclarationContext context, UninitializedVariableType uninitializedVariableType) : base(parent, context) {

			Name = context.ID().GetText();
			UninitializedVariableType = uninitializedVariableType;
			Type = ToType(context.type());
			IsMutable = context.MUTABLE() != null;

			ImmediateChildren = ImmutableArray.Create<ITreeNode>()
			                                  .Add(Type);

		}


		public override string ToJs(Knowledge knowledge) {
			var keyword = UninitializedVariableType switch {
				UninitializedVariableType.Field => throw new InvalidOperationException("Fields are translated to js in different way"),
				UninitializedVariableType.Local when IsMutable => "let",
				UninitializedVariableType.Local when !IsMutable => "const",
				UninitializedVariableType.Parameter => "",
			};
			return $"{keyword} {knowledge.GetNewNameFor(this)};\n";
		}

	}

}