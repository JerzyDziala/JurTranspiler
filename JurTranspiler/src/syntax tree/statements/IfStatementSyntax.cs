using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

	public class IfStatementSyntax : ISyntaxNode, IStatementSyntax {

		//INode
		public ISyntaxNode Root { get; }
		public ISyntaxNode Parent { get; }
		public ImmutableList<ISyntaxNode> AllParents { get; }
		public ImmutableList<ITreeNode> ImmediateChildren { get; }
		public ImmutableList<ITreeNode> AllChildren { get; }

		public string File { get; }
		public int Line { get; }
		public int Abstraction { get; }

		public IExpressionSyntax Condition { get; }
		public IStatementSyntax Body { get; }
		public bool HaveElse { get; }
		public IStatementSyntax ElseBody { get; }


		public IfStatementSyntax(ISyntaxNode parent, JurParser.IfStatementContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;

			Condition = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression());
			Body = new GeneratedScopeSyntax(this, context.statement(0));
			HaveElse = context.ELSE() != null;
			ElseBody = HaveElse
				           ? new GeneratedScopeSyntax(this, context.statement(1))
				           : null;
			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .Add(Condition)
			                                 .Add(Body)
			                                 .AddIfNotNull(ElseBody);

			AllChildren = this.GetAllChildren();

		}


		public string ToJs(Binder binder) {
			return $"if ({Condition.ToJs(binder)}) {Body.ToJs(binder)}" + (HaveElse ? $"else ${ElseBody.ToJs(binder)}" : "");
		}

	}

}