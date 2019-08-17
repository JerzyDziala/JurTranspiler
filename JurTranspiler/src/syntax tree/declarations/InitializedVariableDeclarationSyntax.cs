using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

	public class InitializedVariableDeclarationSyntax : SyntaxNode, IStatementSyntax, IVariableDeclarationSyntax, IAssignment {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

		public string Name { get; }
		public IExpressionSyntax Initializer { get; }
		public ITypeSyntax? Type { get; }


		public InitializedVariableDeclarationSyntax(ISyntaxNode parent, JurParser.InitializedVariableDeclarationContext context) : base(parent, context){
			Name = context.ID().GetText();
			Initializer = ExpressionSyntaxFactory.Create(this, context.expression());
			Type = ToType(context.type());

			ImmediateChildren = ImmutableArray.Create<ITreeNode>()
			                                 .Add(Type)
			                                 .Add(Initializer);
			AllChildren = GetAllChildren();

		}


                public override string ToJs(Knowledge knowledge) {
			return $"let {Name} = {Initializer.ToJs(knowledge)};\n";
		}

	}

}