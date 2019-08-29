using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using UtilityLibrary;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.compilerSource.nodes {

	public class IfStatementSyntax : SyntaxNode, IStatementSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public IExpressionSyntax Condition { get; }
		public IStatementSyntax Body { get; }
		public IStatementSyntax? ElseBody { get; }
		public bool HaveElse => ElseBody != null;



        public IfStatementSyntax(ISyntaxNode parent, JurParser.IfStatementContext context) : base(parent, context) {

			Condition = ExpressionSyntaxFactory.Create(this, context.expression());
			Body = new GeneratedScopeSyntax(this, context.statement(0));
			ElseBody = context.ELSE() != null
				           ? new GeneratedScopeSyntax(this, context.statement(1))
				           : null;

			ImmediateChildren = ImmutableArray.Create<ITreeNode>()
			                                  .Add(Condition)
			                                  .Add(Body)
			                                  .AddIfNotNull(ElseBody);

		}


		public override string ToJs(Knowledge knowledge) {
			var e = HaveElse ? $"else {ElseBody!.ToJs(knowledge)}" : "";
			return $@"if ({Condition.ToJs(knowledge)}) {Body.ToJs(knowledge)}
                      {e}";
		}


    }

}