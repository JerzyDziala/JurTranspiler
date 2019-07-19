using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

    public class BlockStatement : ISyntaxNode, IStatementSyntax {

        public ISyntaxNode Root { get; }
        public ISyntaxNode Parent { get; }
        public ImmutableList<ISyntaxNode> AllParents { get; }
        public ImmutableList<ITreeNode> ImmediateChildren { get; }
        public ImmutableList<ITreeNode> AllChildren { get; }

        public string File { get; }
        public int Line { get; }
        public int Abstraction { get; }

        public ImmutableList<IStatementSyntax> Body { get; }


        public BlockStatement(ISyntaxNode parent, JurParser.BlockStatementContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            Body = context.statement()
                          .Select(x => StatementSyntaxFactory.CreateStatementSyntax(this, x))
                          .ToImmutableList();

            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .AddRange(Body);

            AllChildren = this.GetAllChildren();

        }

        public BlockStatement(ISyntaxNode parent, JurParser.BlockContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            Body = context.statement()
                          .Select(x => StatementSyntaxFactory.CreateStatementSyntax(this, x))
                          .ToImmutableList();

            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .AddRange(Body);

            AllChildren = this.GetAllChildren();

        }

        public string ToJs(Knowledge knowledge) {
	        return $"{{{Body.Select(x => x.ToJs(knowledge)).Glue("\n")}}}";
        }


    }

}