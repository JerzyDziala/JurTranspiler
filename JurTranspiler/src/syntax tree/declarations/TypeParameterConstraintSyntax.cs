using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

    public class TypeParameterConstraintSyntax : SyntaxNode {
        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

        public ITypeSyntax Left { get; }
        public ITypeSyntax Right { get; }


        public TypeParameterConstraintSyntax(ISyntaxNode parent, JurParser.ConstrainContext context) : base(parent, context) {

            Left = ToType(context.type(0));
            Right = ToType(context.type(1));

            ImmediateChildren = ImmutableArray.Create<ITreeNode>()
                                              .Add(Left)
                                              .Add(Right);
            AllChildren = GetAllChildren();
        }


        public override string ToJs(Knowledge knowledge) {
            throw new System.NotImplementedException();
        }

    }

}