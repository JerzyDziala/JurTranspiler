using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.nodes {

    public class ParenthesisSyntax : IExpressionSyntax, ISyntaxNode {

        public override ISyntaxNode Root { get; }
        public override ISyntaxNode Parent { get; }
        public override ImmutableList<ISyntaxNode> AllParents { get; }
        public override ImmutableList<ITreeNode> ImmediateChildren { get; }
        public override ImmutableList<ITreeNode> AllChildren { get; }
        public override string File { get; }
        public override int Line { get; }
        public override int Abstraction { get; }

        public IExpressionSyntax Expression { get; }


        public ParenthesisSyntax(ISyntaxNode parent, JurParser.ParExpressionContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            Expression = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression());
            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .Add(Expression);
            AllChildren = this.GetAllChildren();

        }


        public override Type Evaluate(HashSet<Error> errors, Binder binder) => Expression.Evaluate(errors,binder);
        public override string ToJs(Binder binder) {
            return $"({Expression.ToJs(binder)})";
        }

    }

}