using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.factories;
using JurTranspiler.syntax_tree.Interfaces;
using UtilityLibrary;

namespace JurTranspiler.syntax_tree.statements {

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