using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

    public class UninitializedVariableDeclarationSyntax : SyntaxNode, IStatementSyntax, IVariableDeclarationSyntax {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }


        public string Name { get; }
        public ITypeSyntax? Type { get; }
        public bool IsParameter { get; }


        public UninitializedVariableDeclarationSyntax(ISyntaxNode parent, JurParser.UninitializedVarDeclarationContext context, bool isParameter) : base(parent, context) {

            Name = context.ID().GetText();
            IsParameter = isParameter;
            Type = ToType(context.type());

            ImmediateChildren = ImmutableArray.Create<ITreeNode>()
                                              .Add(Type);

        }


        public override string ToJs(Knowledge knowledge) {
            return $"let {Name};\n";
        }

    }

}