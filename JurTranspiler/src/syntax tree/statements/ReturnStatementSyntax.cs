using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;
using UtilityLibrary;

namespace JurTranspiler.syntax_tree.statements {

    public class ReturnStatementSyntax : SyntaxNode, IStatementSyntax {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

        public IExpressionSyntax? ReturnValue { get; }
        public bool IsVoid => ReturnValue == null;


        public ReturnStatementSyntax(ISyntaxNode parent, JurParser.ReturnStatementContext context) : base(parent, context) {

            if (context.expression() != null) {
                ReturnValue = ToExpression(context.expression());
            }

            ImmediateChildren = ImmutableArray.Create<ITreeNode>().AddIfNotNull(ReturnValue);

        }


        public override string ToJs(Knowledge knowledge) {
            return $"return{(IsVoid ? "" : $" {ReturnValue!.ToJs(knowledge)}")};\n";
        }

    }

}