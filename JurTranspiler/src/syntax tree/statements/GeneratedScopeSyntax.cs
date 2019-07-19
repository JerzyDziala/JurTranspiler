using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

    public class GeneratedScopeSyntax : ISyntaxNode, IStatementSyntax {

        public ISyntaxNode Root { get; }
        public ISyntaxNode Parent { get; }
        public ImmutableList<ISyntaxNode> AllParents { get; }
        public ImmutableList<ITreeNode> ImmediateChildren { get; }
        public ImmutableList<ITreeNode> AllChildren { get; }

        public string File { get; }
        public int Line { get; }
        public int Abstraction { get; }

        public IStatementSyntax Body { get; }


        public GeneratedScopeSyntax(ISyntaxNode parent, JurParser.StatementContext body) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = parent.Line;

            Body = StatementSyntaxFactory.CreateStatementSyntax(this, body);

            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .AddIfNotNull(Body);

            AllChildren = this.GetAllChildren();

        }

        public string ToJs(Knowledge knowledge) {
            return Body.ToJs(knowledge);
        }


    }

}