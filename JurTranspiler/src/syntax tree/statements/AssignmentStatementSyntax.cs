using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.statements {

    public class AssignmentStatementSyntax : SyntaxNode, IAssignment {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }


        public IExpressionSyntax Left { get; }
        public IExpressionSyntax Right { get; }


        public AssignmentStatementSyntax(ISyntaxNode parent, JurParser.AssignmentStatementContext context) : base(parent, context) {

            Left = ToExpression(context.expression(0));
            Right = ToExpression(context.expression(1));

            ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(Left).Add(Right);

        }


        public override string ToJs(Knowledge knowledge) {
            return $"{Left.ToJs(knowledge)} = {Right.ToJs(knowledge)};\n";
        }

    }

}