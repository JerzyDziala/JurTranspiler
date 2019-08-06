using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;

namespace JurTranspiler.compilerSource.nodes {

    public class TypeExpressionSyntax : IExpressionSyntax {

        public override ISyntaxNode Root { get; }
        public override ISyntaxNode Parent { get; }
        public override ImmutableList<ISyntaxNode> AllParents { get; }
        public override ImmutableList<ITreeNode> ImmediateChildren { get; }
        public override ImmutableList<ITreeNode> AllChildren { get; }
        public override string File { get; }
        public override int Line { get; }
        public override int Abstraction { get; }

        public ITypeSyntax Type { get; }


        public TypeExpressionSyntax(ISyntaxNode parent, JurParser.TypeExpressionContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.TYPE().Symbol.Line;

            Type = TypeSyntaxFactory.Create(this, context.type());

            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .Add(Type);
            AllChildren = this.GetAllChildren();
        }


        public override string ToJs(Knowledge knowledge) =>
            AllParents.Any(x => x is FunctionDefinitionSyntax function && function.IsGeneric)
                ? $"types['{Type.FullName}'].withSubstitutedTypes(substitutions)"
                : $"types['{Type.FullName}']";
    }

}