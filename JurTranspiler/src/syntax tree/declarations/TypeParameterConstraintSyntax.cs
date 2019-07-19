using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;

namespace JurTranspiler.compilerSource.nodes {

    public class TypeParameterConstraintSyntax : ISyntaxNode {

        public ISyntaxNode Root { get; }
        public ISyntaxNode Parent { get; }
        public ImmutableList<ISyntaxNode> AllParents { get; }
        public ImmutableList<ITreeNode> ImmediateChildren { get; }
        public ImmutableList<ITreeNode> AllChildren { get; }
        public string File { get; }
        public int Line { get; }
        public int Abstraction { get; }

        public ITypeSyntax Left { get; }
        public ITypeSyntax Right { get; }


        public TypeParameterConstraintSyntax(ISyntaxNode parent, JurParser.ConstrainContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            Left = TypeSyntaxFactory.Create(this, context.type(0));
            Right = TypeSyntaxFactory.Create(this, context.type(1));

            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .Add(Left)
                                             .Add(Right);

            AllChildren = this.GetAllChildren();
        }

        public string ToJs(Knowledge knowledge) {
            throw new System.NotImplementedException();
        }


    }

}