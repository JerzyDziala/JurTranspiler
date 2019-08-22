using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.CodeGeneration;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.src.syntax_tree.types;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

    public class ConstructorSyntax : SyntaxNode, IExpressionSyntax {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

        public ITypeSyntax ConstructedType { get; }


        public ConstructorSyntax(ISyntaxNode parent, JurParser.ConstructorContext context) : base(parent, context) {

            ConstructedType = ToType(context.type());
            ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(ConstructedType);

        }


        public override string ToJs(Knowledge knowledge) {
            var needsSubstitutions = ConstructedType.AllChildren.Any(x => x is TypeParameterSyntax);
            var type = "_t_[" + ConstructedType.FullName.AsString() + "]";

            if (needsSubstitutions) {
                type += "._wst_(_s_)";
            }
            return type + ".createInstance()";
        }

    }

}