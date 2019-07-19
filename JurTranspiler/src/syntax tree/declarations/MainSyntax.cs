using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.compilerSource.nodes {

    public class MainSyntax : ISyntaxNode {

        //INode
        public ISyntaxNode Root { get; }
        public ISyntaxNode Parent { get; }
        public ImmutableList<ISyntaxNode> AllParents { get; }
        public ImmutableList<ITreeNode> ImmediateChildren { get; }
        public ImmutableList<ITreeNode> AllChildren { get; }

        public string File { get; }
        public int Line { get; }
        public int Abstraction { get; }

        public IStatementSyntax Body { get; }


        public MainSyntax(ISyntaxNode parent, JurParser.MainContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = int.MaxValue;
            File = parent.File;
            Line = context.Start.Line;

            Body = StatementSyntaxFactory.CreateStatementSyntax(this, context.statement());
            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .Add(Body);

            AllChildren = this.GetAllChildren();

        }


        public string ToJs(Knowledge knowledge) {
            return $"function main$(){Body.ToJs(knowledge)}";
        }

    }

}