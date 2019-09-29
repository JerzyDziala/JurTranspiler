using System.Collections.Immutable;
using Antlr4.Runtime;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.declarations {

	public class UninitializedVariableDeclarationSyntax : SyntaxNode, IStatementSyntax, IVariableDeclarationSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public string Name { get; }
		public ITypeSyntax? Type { get; }
		public bool IsParameter { get; }


		public UninitializedVariableDeclarationSyntax(ISyntaxNode parent, JurParser.UninitializedVarDeclarationContext context, bool isParameter) : base(parent, context) {

			Name = context.ID().GetText();
			IsParameter = isParameter;
			Type = ToType(context.type());

			ImmediateChildren = ImmutableArray.Create<ITreeNode>()
			                                  .Add(Type);

		}


		public UninitializedVariableDeclarationSyntax(ISyntaxNode parent,
		                                              ITypeSyntax type,
		                                              string name,
		                                              ParserRuleContext context) : base(parent, context) {

			Name = name;
			IsParameter = false;
			Type = type;

			ImmediateChildren = ImmutableArray.Create<ITreeNode>()
			                                  .Add(Type);

		}


		public override string ToJs(Knowledge knowledge) {
			return $"let {knowledge.GetNewNameFor(this)};\n";
		}

	}

}