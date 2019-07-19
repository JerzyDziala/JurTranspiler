using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.nodes {

    public class AssignmentStatementSyntax : IAssignment {

        //INode
        public ISyntaxNode Root { get; }
        public ISyntaxNode Parent { get; }
        public ImmutableList<ISyntaxNode> AllParents { get; }
        public ImmutableList<ITreeNode> ImmediateChildren { get; }
        public ImmutableList<ITreeNode> AllChildren { get; }

        public string File { get; }
        public int Line { get; }
        public int Abstraction { get; }

        public IExpressionSyntax Left { get; }
        public IExpressionSyntax Right { get; }


        public AssignmentStatementSyntax(ISyntaxNode parent, JurParser.AssignmentStatementContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            Left = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression(0));
            Right = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression(1));

            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .Add(Left)
                                             .Add(Right);

            AllChildren = this.GetAllChildren();

        }

        public string ToJs(Knowledge knowledge) {
            return $"{Left.ToJs(knowledge)} = {Right.ToJs(knowledge)};\n";
        }

    }

}