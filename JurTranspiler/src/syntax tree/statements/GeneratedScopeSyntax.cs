using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using UtilityLibrary;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

    public class GeneratedScopeSyntax : SyntaxNode, IStatementSyntax {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }


        public IStatementSyntax Body { get; }


        public GeneratedScopeSyntax(ISyntaxNode parent, JurParser.StatementContext body) : base(parent, body, parent.Line) {
            Body = ToStatement(body);
            ImmediateChildren = ImmutableArray.Create<ITreeNode>().AddIfNotNull(Body);

        }


        public override string ToJs(Knowledge knowledge) {
            return Body.ToJs(knowledge);
        }

    }

}