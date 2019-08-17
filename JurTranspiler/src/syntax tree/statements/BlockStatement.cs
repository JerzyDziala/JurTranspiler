using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.bases;
using UtilityLibrary;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

    public class BlockStatement : SyntaxNode, IStatementSyntax {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

        public ImmutableArray<IStatementSyntax> Body { get; }


        public BlockStatement(ISyntaxNode parent, JurParser.BlockStatementContext context) : base(parent, context) {
            Body = ToStatements(context.statement());
            ImmediateChildren = ImmutableArray.Create<ITreeNode>().AddRange(Body);
            AllChildren = GetAllChildren();
        }


        public BlockStatement(ISyntaxNode parent, JurParser.BlockContext context) : base(parent, context) {
            Body = ToStatements(context.statement());
            ImmediateChildren = ImmutableArray.Create<ITreeNode>().AddRange(Body);
            AllChildren = GetAllChildren();
        }


        public override string ToJs(Knowledge knowledge) {
            return $"{{{Body.Select(x => x.ToJs(knowledge)).Glue("\n")}}}";
        }

    }

}