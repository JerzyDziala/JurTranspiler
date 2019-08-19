using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

    public class ParenthesisSyntax : SyntaxNode, IExpressionSyntax {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }


        public IExpressionSyntax Expression { get; }


        public ParenthesisSyntax(ISyntaxNode parent, JurParser.ParExpressionContext context) : base(parent, context) {
            Expression = ToExpression(context.expression());
            ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(Expression);

        }


        public override string ToJs(Knowledge knowledge) {
            return $"({Expression.ToJs(knowledge)})";
        }

    }

}