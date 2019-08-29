using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.compilerSource.nodes {

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