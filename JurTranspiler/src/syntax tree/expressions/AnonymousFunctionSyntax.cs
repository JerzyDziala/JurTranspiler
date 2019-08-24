using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.CodeGeneration;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.syntax_tree.bases;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

    public class AnonymousFunctionSyntax : SyntaxNode, IExpressionSyntax {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

        public ImmutableArray<UninitializedVariableDeclarationSyntax> Parameters { get; }
        public IStatementSyntax Body { get; }
        public bool IsExpressionStatementLambda { get; }


        public AnonymousFunctionSyntax(ISyntaxNode parent, JurParser.AnonymusFunctionContext context) : base(parent, context) {

            Parameters = context.uninitializedVarDeclaration().Select(x => new UninitializedVariableDeclarationSyntax(this, x, true)).ToImmutableArray();

            Body = context.expression() != null
                       ? new ExpressionStatementSyntax(this, context.expression())
                       : (IStatementSyntax) new BlockStatement(this, context.block());

            IsExpressionStatementLambda = Body is ExpressionStatementSyntax;

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

            return (Body is ExpressionStatementSyntax statement
                        ? knowledge.ExpressionsBindings[statement.ExpressionSyntax] is VoidType
                              ? $"function({Parameters.Select(knowledge.GetNewNameFor).Glue(", ")}) {{{statement.ExpressionSyntax.ToJs(knowledge)};}}"
                              : $"function({Parameters.Select(knowledge.GetNewNameFor).Glue(", ")}) {{return {statement.ExpressionSyntax.ToJs(knowledge)};}}"
                        : $"function({Parameters.Select(knowledge.GetNewNameFor).Glue(", ")}) {{{Body.ToJs(knowledge)}}}")
                .WithTypeNameNoParents($"{t}.name");
        }

    }

}