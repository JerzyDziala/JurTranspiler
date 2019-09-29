using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;
using UtilityLibrary;

namespace JurTranspiler.syntax_tree.statements {

    public class BlockStatement : SyntaxNode, IStatementSyntax {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

        public ImmutableArray<IStatementSyntax> Body { get; }


        public BlockStatement(ISyntaxNode parent, JurParser.BlockStatementContext context) : base(parent, context) {
            Body = ToStatements(context.statement());
            ImmediateChildren = ImmutableArray.Create<ITreeNode>().AddRange(Body);

        }


        public BlockStatement(ISyntaxNode parent, JurParser.BlockContext context) : base(parent, context) {
            Body = ToStatements(context.statement());
            ImmediateChildren = ImmutableArray.Create<ITreeNode>().AddRange(Body);

        }


        public override string ToJs(Knowledge knowledge) {
            return $"{{{Body.Select(x => x.ToJs(knowledge)).Glue("\n")}}}";
        }

    }

}