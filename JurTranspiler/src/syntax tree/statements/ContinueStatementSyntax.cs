using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

    public class ContinueStatementSyntax : SyntaxNode, IStatementSyntax {
        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }


        public ContinueStatementSyntax(ISyntaxNode parent, JurParser.ContinueStatementContext context) : base(parent, context) {
            ImmediateChildren = ImmutableArray.Create<ITreeNode>();
            AllChildren = GetAllChildren();
        }


        public override string ToJs(Knowledge knowledge) {
            return "continue;\n";
        }

    }

}