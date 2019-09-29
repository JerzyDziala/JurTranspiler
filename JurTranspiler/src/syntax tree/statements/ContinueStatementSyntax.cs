using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.statements {

    public class ContinueStatementSyntax : SyntaxNode, IStatementSyntax {
        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }



        public ContinueStatementSyntax(ISyntaxNode parent, JurParser.ContinueStatementContext context) : base(parent, context) {
            ImmediateChildren = ImmutableArray.Create<ITreeNode>();

        }


        public override string ToJs(Knowledge knowledge) {
            return "continue;\n";
        }

    }

}