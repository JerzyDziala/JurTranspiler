using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

    public class VariableAccessSyntax : SyntaxNode, IExpressionSyntax {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

        public string Name { get; }


        public VariableAccessSyntax(ISyntaxNode parent, JurParser.VariableAccessContext context) : base(parent, context) {
            Name = context.ID().GetText();

            ImmediateChildren = ImmutableArray.Create<ITreeNode>();
            AllChildren = GetAllChildren();
        }


        public override string ToJs(Knowledge knowledge) {
            return Name;
        }

    }

}