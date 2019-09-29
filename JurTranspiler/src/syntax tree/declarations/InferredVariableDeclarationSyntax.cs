using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.factories;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.declarations {

    public class InferredVariableDeclarationSyntax : SyntaxNode, IStatementSyntax, IVariableDeclarationSyntax {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

        public string Name { get; }
        public IExpressionSyntax Initializer { get; }
        public ITypeSyntax? Type => null;


        public InferredVariableDeclarationSyntax(ISyntaxNode parent, JurParser.InferedVariableDeclarationContext context) : base(parent, context) {

            Name = context.ID().GetText();
            Initializer = ExpressionSyntaxFactory.Create(this, context.expression());

            ImmediateChildren = ImmutableArray.Create<ITreeNode>()
                                              .Add(Initializer);

        }


        public override string ToJs(Knowledge knowledge) {
            return $"let {knowledge.GetNewNameFor(this)} = {Initializer.ToJs(knowledge)};\n";
        }

    }

}