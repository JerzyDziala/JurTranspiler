using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Antlr4.Runtime;
using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

	public class FieldAccessSyntax : IExpressionSyntax, ISyntaxNode {

		//INode
		public override ISyntaxNode Root { get; }
		public override ISyntaxNode Parent { get; }
		public override ImmutableList<ISyntaxNode> AllParents { get; }
		public override ImmutableList<ITreeNode> ImmediateChildren { get; }
		public override ImmutableList<ITreeNode> AllChildren { get; }
		public override string File { get; }
		public override int Line { get; }
		public override int Abstraction { get; }

		public IExpressionSyntax Owner { get; }
		public string Name { get; }


		public FieldAccessSyntax(ISyntaxNode parent, JurParser.FieldAccessContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;

			Owner = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression());
			Name = context.ID().GetText();
			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .Add(Owner);
			AllChildren = this.GetAllChildren();
		}




                public override string ToJs(Knowledge knowledge) {
			return $"{Owner.ToJs(knowledge)}.{Name}";
		}

	}

}