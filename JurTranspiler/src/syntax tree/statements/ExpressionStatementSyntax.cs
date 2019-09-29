using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.statements {

    public class ExpressionStatementSyntax : SyntaxNode, IStatementSyntax {
        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

        public IExpressionSyntax ExpressionSyntax { get; }


        public ExpressionStatementSyntax(ISyntaxNode parent, JurParser.ExpressionStatementContext context) : base(parent, context) {
            ExpressionSyntax = ToExpression(context.expression());
            ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(ExpressionSyntax);

        }


        public ExpressionStatementSyntax(ISyntaxNode parent, JurParser.ExpressionContext context) : base(parent, context) {
            ExpressionSyntax = ToExpression(context);
            ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(ExpressionSyntax);

        }


        public override string ToJs(Knowledge knowledge) {
            return ExpressionSyntax.ToJs(knowledge) + ";\n";
        }

    }

}