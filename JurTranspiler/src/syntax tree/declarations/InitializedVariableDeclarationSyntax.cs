using System.Collections.Immutable;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.factories;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.declarations {

    public class InitializedVariableDeclarationSyntax : SyntaxNode, IVariableDeclarationSyntax, IAssignment {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

        public string Name { get; }
        public IExpressionSyntax Initializer { get; }
        public ITypeSyntax? Type { get; }
        public bool IsMutable { get; }


        public InitializedVariableDeclarationSyntax(ISyntaxNode parent, JurParser.InitializedVariableDeclarationContext context) : base(parent, context) {
            Name = context.ID().GetText();
            Initializer = ExpressionSyntaxFactory.Create(this, context.expression());
            Type = ToType(context.type());
            IsMutable = context.MUTABLE() != null;

            ImmediateChildren = ImmutableArray.Create<ITreeNode>()
                                              .Add(Type)
                                              .Add(Initializer);

        }


        public override string ToJs(Knowledge knowledge) {
            var keyword = IsMutable ? "let" : "const";
            return $"{keyword} {knowledge.GetNewNameFor(this)} = {Initializer.ToJs(knowledge)};\n";
        }

    }

}