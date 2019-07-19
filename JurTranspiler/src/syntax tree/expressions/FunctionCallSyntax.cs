using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.nodes {

	public class FunctionCallSyntax : IExpressionSyntax, ISyntaxNode {

		public override ISyntaxNode Root { get; }
		public override ISyntaxNode Parent { get; }
		public override ImmutableList<ISyntaxNode> AllParents { get; }
		public override ImmutableList<ITreeNode> ImmediateChildren { get; }
		public override ImmutableList<ITreeNode> AllChildren { get; }
		public override string File { get; }
		public override int Line { get; }
		public override int Abstraction { get; }

		public string Name { get; }
		public bool IsPoly { get; }
		public ImmutableList<ITypeSyntax> ExplicitTypeArguments { get; }
		public bool HasExplicitTypeArguments => ExplicitTypeArguments.Any();
		public ImmutableList<IExpressionSyntax> Arguments { get; }


		public FunctionCallSyntax(ISyntaxNode parent, JurParser.FunctionCallContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;

			Name = context.ID().GetText();
			Arguments = context.expression().Select(x => ExpressionSyntaxFactory.CreateExpressionSyntax(this, x)).ToImmutableList();
			ExplicitTypeArguments = context.type().Select(x => TypeSyntaxFactory.Create(this, x)).ToImmutableList();
			IsPoly = context.POLY() != null;
			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .AddRange(Arguments)
			                                 .AddRange(ExplicitTypeArguments);
			AllChildren = this.GetAllChildren();

		}




                public override string ToJs(Knowledge knowledge) {
			if (IsPoly) throw new NotImplementedException("poly methods are not supported by code gen");

			var args = $"{Arguments.Select(x => x.ToJs(knowledge)).Glue(", ")}";
			return $"{knowledge.GetNewNameFor(this)}({args})";
		}



	}

}