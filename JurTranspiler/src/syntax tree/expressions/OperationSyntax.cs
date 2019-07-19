using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.semantic_model;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.nodes {

    public class OperationSyntax : IExpressionSyntax, ISyntaxNode {

        public override ISyntaxNode Root { get; }
        public override ISyntaxNode Parent { get; }
        public override ImmutableList<ISyntaxNode> AllParents { get; }
        public override ImmutableList<ITreeNode> ImmediateChildren { get; }
        public override ImmutableList<ITreeNode> AllChildren { get; }
        public override string File { get; }
        public override int Line { get; }
        public override int Abstraction { get; }

        public string Operator { get; }
        public IExpressionSyntax Left { get; }
        public IExpressionSyntax Right { get; }


        public OperationSyntax(ISyntaxNode parent, JurParser.OperationContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            var op = context.@operator?.Text ?? context.LOGICAL_AND()?.GetText() ?? context.OR()?.GetText();

            Operator = op;
            Left = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression(0));
            Right = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression(1));
            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .Add(Left)
                                             .Add(Right);
            AllChildren = this.GetAllChildren();

        }


        public override string ToJs(Knowledge knowledge) {
            return $"{Left.ToJs(knowledge)} {Operator} {Right.ToJs(knowledge)}";
        }

    }

}