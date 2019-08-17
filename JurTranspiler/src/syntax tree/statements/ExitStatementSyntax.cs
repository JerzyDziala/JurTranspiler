using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

    public class ExitStatementSyntax : SyntaxNode, IStatementSyntax {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

        public string Message { get; }


        public ExitStatementSyntax(ISyntaxNode parent, JurParser.ExitStatementContext context) : base(parent, context) {

            Message = context.STRING_VALUE().GetText();

            ImmediateChildren = ImmutableArray.Create<ITreeNode>();
            AllChildren = GetAllChildren();
        }


        public override string ToJs(Knowledge knowledge) {
            return $"throw new Error(\"{Message}\");\n";
        }

    }

}