using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.declarations {

    public class TypeParameterConstraintSyntax : SyntaxNode {
        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }


        public ITypeSyntax Left { get; }
        public ITypeSyntax Right { get; }


        public TypeParameterConstraintSyntax(ISyntaxNode parent, JurParser.ConstrainContext context) : base(parent, context) {

            Left = ToType(context.type(0));
            Right = ToType(context.type(1));

            ImmediateChildren = ImmutableArray.Create<ITreeNode>()
                                              .Add(Left)
                                              .Add(Right);

        }


        public override string ToJs(Knowledge knowledge) {
            throw new System.NotImplementedException();
        }

    }

}