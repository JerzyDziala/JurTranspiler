using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.nodes {

	public class InitializedVariableDeclarationSyntax : ISyntaxNode, IStatementSyntax, IVariableDeclarationSyntax, IAssignment {

		//INode
		public ISyntaxNode Root { get; }
		public ISyntaxNode Parent { get; }
		public ImmutableList<ISyntaxNode> AllParents { get; }
		public ImmutableList<ITreeNode> ImmediateChildren { get; }
		public ImmutableList<ITreeNode> AllChildren { get; }
		public string File { get; }
		public int Line { get; }
		public int Abstraction { get; }

		public string Name { get; }
		public IExpressionSyntax Initializer { get; }
		public ITypeSyntax Type { get; }


		public InitializedVariableDeclarationSyntax(ISyntaxNode parent, JurParser.InitializedVariableDeclarationContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;
			Name = context.ID().GetText();
			Initializer = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression());
			Type = TypeSyntaxFactory.Create(this, context.type());

			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .Add(Type)
			                                 .Add(Initializer);
			AllChildren = this.GetAllChildren();

		}


		public Type GetDeclaredType(HashSet<Error> errors, Binder binder) => binder.BindType(Type, errors);

		public Type GetLeftType(HashSet<Error> errors, Binder binder) => GetDeclaredType(errors, binder);

		public Type GetRightType(HashSet<Error> errors, Binder binder) => Initializer.Evaluate(errors, binder);


		public string ToJs(Binder binder) {
			return $"let {Name} = {Initializer.ToJs(binder)}";
		}

	}

}