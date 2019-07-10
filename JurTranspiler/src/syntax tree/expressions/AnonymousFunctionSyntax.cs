using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Mime;
using System.Xml;
using System.Xml.Serialization;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.src.syntax_tree.types;
using UtilityLibrary;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.nodes {

	public class AnonymousFunctionSyntax : IExpressionSyntax {

		public override ISyntaxNode Root { get; }
		public override ISyntaxNode Parent { get; }
		public override ImmutableList<ISyntaxNode> AllParents { get; }
		public override ImmutableList<ITreeNode> ImmediateChildren { get; }
		public override ImmutableList<ITreeNode> AllChildren { get; }
		public override string File { get; }
		public override int Line { get; }
		public override int Abstraction { get; }

		public ImmutableList<UninitializedVariableDeclarationSyntax> Parameters { get; }
		public IStatementSyntax Body { get; }
		public bool IsExpressionStatementLambda { get; }


		public AnonymousFunctionSyntax(ISyntaxNode parent, JurParser.AnonymusFunctionContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;

			Parameters = context.uninitializedVarDeclaration().Select(x => new UninitializedVariableDeclarationSyntax(this, x, true)).ToImmutableList();

			Body = context.expression() != null
				       ? new ExpressionStatementSyntax(this, context.expression())
				       : (IStatementSyntax) new BlockStatement(this, context.block());

			IsExpressionStatementLambda = Body is ExpressionStatementSyntax;

			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .AddRange(Parameters)
			                                 .AddIfNotNull(Body);

			AllChildren = this.GetAllChildren();

		}


		public override Type Evaluate(HashSet<Error> errors, Binder binder) {

			Type returnType;
			if (IsExpressionStatementLambda) {
				returnType = ((ExpressionStatementSyntax) Body).ExpressionSyntax.Evaluate(errors, binder);
			}
			else {
				var returnStatement = ((BlockStatement) Body).Body.LastOrDefault(statement => statement is ReturnStatementSyntax) as ReturnStatementSyntax;
				returnType = returnStatement != null && !returnStatement.IsVoid
					             ? returnStatement.ReturnValue.Evaluate(errors, binder)
					             : new VoidType();
			}

			return new FunctionPointerType(returnType, Parameters.Select(x => binder.BindType(x.Type, errors)));
		}


		public override string ToJs(Binder binder) {
			return $"function({Parameters.Select(x => x.Name).Glue(", ")}){{\n{Body.ToJs(binder)}}}";
		}

	}

}