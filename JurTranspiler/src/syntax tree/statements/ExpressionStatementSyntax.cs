using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

    public class ExpressionStatementSyntax : SyntaxNode, IStatementSyntax {
        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

        public IExpressionSyntax ExpressionSyntax { get; }


        public ExpressionStatementSyntax(ISyntaxNode parent, JurParser.ExpressionStatementContext context) : base(parent, context) {
            ExpressionSyntax = ToExpression(context.expression());
            ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(ExpressionSyntax);
            AllChildren = GetAllChildren();
        }


        public ExpressionStatementSyntax(ISyntaxNode parent, JurParser.ExpressionContext context) : base(parent, context) {
            ExpressionSyntax = ToExpression(context);
            ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(ExpressionSyntax);
            AllChildren = GetAllChildren();
        }


        public override string ToJs(Knowledge knowledge) {
            return ExpressionSyntax.ToJs(knowledge) + ";\n";
        }

    }

}