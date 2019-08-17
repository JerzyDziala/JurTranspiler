using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

    public class FieldAccessSyntax : SyntaxNode, IExpressionSyntax {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

        public IExpressionSyntax Owner { get; }
        public string Name { get; }


        public FieldAccessSyntax(ISyntaxNode parent, JurParser.FieldAccessContext context) : base(parent, context) {

            Owner = ExpressionSyntaxFactory.Create(this, context.expression());
            Name = context.ID().GetText();
            ImmediateChildren = ImmutableArray.Create<ITreeNode>()
                                              .Add(Owner);
            AllChildren = GetAllChildren();
        }


        public override string ToJs(Knowledge knowledge) {
            return $"{Owner.ToJs(knowledge)}.{Name}";
        }

    }

}