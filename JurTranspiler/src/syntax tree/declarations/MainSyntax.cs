using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

    public class MainSyntax : SyntaxNode {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

        public IStatementSyntax Body { get; }


        public MainSyntax(ISyntaxNode parent, JurParser.MainContext context) : base(int.MaxValue, parent, context) {

            Body = StatementSyntaxFactory.Create(this, context.statement());

            ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(Body);
            AllChildren = GetAllChildren();
        }


        public override string ToJs(Knowledge knowledge) {
            return $"function main$(){Body.ToJs(knowledge)}";
        }

    }

}