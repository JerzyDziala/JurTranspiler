using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.compilerSource.nodes {

    public class ExitStatement : ISyntaxNode, IStatementSyntax {
        public ISyntaxNode Root { get; }
        public ISyntaxNode Parent { get; }
        public ImmutableList<ISyntaxNode> AllParents { get; }
        public ImmutableList<ITreeNode> ImmediateChildren { get; }
        public ImmutableList<ITreeNode> AllChildren { get; }

        public string File { get; }
        public int Line { get; }
        public int Abstraction { get; }
        public string Message { get; }


        public ExitStatement(ISyntaxNode parent, JurParser.ExitStatementContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            Message = context.STRING_VALUE().GetText();

            ImmediateChildren = ImmutableList.Create<ITreeNode>();

            AllChildren = this.GetAllChildren();

        }

        public string ToJs(Binder binder) {
            return $"throw new Error(\"{Message}\")";
        }

    }

}