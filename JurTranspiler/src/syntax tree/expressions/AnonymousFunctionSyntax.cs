using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.CodeGeneration;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.syntax_tree.bases;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

	public class AnonymousFunctionSyntax : ExpressionSyntax , IFunctionDefinitionOrLambdaSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public ImmutableArray<UninitializedVariableDeclarationSyntax> Parameters { get; }
		public IStatementSyntax? Body { get; }
		public bool IsArrow => Body is ExpressionStatementSyntax;
		public bool IsExtern => false;

		public ImmutableArray<ReturnStatementSyntax> TopLevelReturns => AllChildren
		                                                                .OfType<ReturnStatementSyntax>()
		                                                                .Where(x => ReferenceEquals(x.EnclosingFunctionOrLambda, this))
		                                                                .ToImmutableArray();


		public AnonymousFunctionSyntax(ISyntaxNode parent, JurParser.AnonymusFunctionContext context) : base(parent, context) {

			Parameters = context.uninitializedVarDeclaration().Select(x => new UninitializedVariableDeclarationSyntax(this, x, true)).ToImmutableArray();

			Body = context.expression() != null
				       ? new ExpressionStatementSyntax(this, context.expression())
				       : (IStatementSyntax) new BlockStatement(this, context.block());

			ImmediateChildren = ImmutableArray.Create<ITreeNode>()
			                                  .AddRange(Parameters)
			                                  .AddIfNotNull(Body);

		}


		public override string ToJs(Knowledge knowledge) {

			var lambdaType = knowledge.ExpressionsBindings[this];

			var needsSubstitutions = lambdaType.AllChildren.Any(x => x is TypeParameterType);

			var t = $"_t_['{lambdaType.Name}']";

			if (needsSubstitutions) {
				t += "._wst_(_s_)";
			}

			var parametersString = Parameters.Select(knowledge.GetNewNameFor).Glue(", ");

			return (Body is ExpressionStatementSyntax statement
				        ? knowledge.ExpressionsBindings[statement.ExpressionSyntax] is VoidType
					          ? $"function({parametersString}) {{{statement.ExpressionSyntax.ToJs(knowledge)};}}"
					          : $"function({parametersString}) {{return {statement.ExpressionSyntax.ToJs(knowledge)};}}"
				        : $"function({parametersString}) {{{Body!.ToJs(knowledge)}}}")
				.WithTypeNameNoParents($"{t}.name");
		}

	}

}