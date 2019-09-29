using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;
using UtilityLibrary;

namespace JurTranspiler.syntax_tree.statements {

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