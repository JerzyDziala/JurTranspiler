using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.statements {

    public class BreakStatementSyntax : SyntaxNode, IStatementSyntax {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }


        public BreakStatementSyntax(ISyntaxNode parent, JurParser.BreakStatementContext context): base(parent,context) {
            ImmediateChildren = ImmutableArray.Create<ITreeNode>();

        }

        public override string ToJs(Knowledge knowledge) {
            return "break;\n";
        }
    }

}