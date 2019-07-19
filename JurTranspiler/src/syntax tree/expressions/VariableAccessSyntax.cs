using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.nodes {

	public class VariableAccessSyntax : IExpressionSyntax {

		public override ISyntaxNode Root { get; }
		public override ISyntaxNode Parent { get; }
		public override ImmutableList<ISyntaxNode> AllParents { get; }
		public override ImmutableList<ITreeNode> ImmediateChildren { get; }
		public override ImmutableList<ITreeNode> AllChildren { get; }
		public override string File { get; }
		public override int Line { get; }
		public override int Abstraction { get; }

		public string Name { get; }


		public VariableAccessSyntax(ISyntaxNode parent, JurParser.VariableAccessContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;

			Name = context.ID().GetText();
			ImmediateChildren = ImmutableList.Create<ITreeNode>();
			AllChildren = this.GetAllChildren();
		}


                public override string ToJs(Knowledge knowledge) {
			return Name;
		}

	}

}