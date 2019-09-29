using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.factories;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.declarations {

    public class MainSyntax : SyntaxNode {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }


        public IStatementSyntax Body { get; }


        public MainSyntax(ISyntaxNode parent, JurParser.MainContext context) : base(int.MaxValue, parent, context) {

            Body = StatementSyntaxFactory.Create(this, context.statement());

            ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(Body);

        }


        public override string ToJs(Knowledge knowledge) {
            return $"function main$(){Body.ToJs(knowledge)}";
        }

    }

}