using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

    public class ArrayIndexAccessSyntax : SyntaxNode, IExpressionSyntax {

        //INode
        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

        public IExpressionSyntax Array { get; }
        public IExpressionSyntax Index { get; }


        public ArrayIndexAccessSyntax(ISyntaxNode parent, JurParser.ArrayIndexAccessContext context) : base(parent, context) {

            Array = ToExpression(context.expression(0));
            Index = ToExpression(context.expression(1));

            ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(Array).Add(Index);
            AllChildren = GetAllChildren();
        }


        public override string ToJs(Knowledge knowledge) {
            return $"{Array.ToJs(knowledge)}[{Index.ToJs(knowledge)}]";
        }

    }

}