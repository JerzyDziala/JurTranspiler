using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

    public class DefaultTypeValueSyntax : SyntaxNode, IExpressionSyntax {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }


        public ITypeSyntax Type { get; }


        public DefaultTypeValueSyntax(ISyntaxNode parent, JurParser.DefaultValueContext context) : base(parent, context, context.DEFAULT_VALUE()) {

            Type = ToType(context.type());

            ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(Type);

        }


        public override string ToJs(Knowledge knowledge) =>
            AllParents.Any(x => x is FunctionDefinitionSyntax function && function.IsGeneric)
                ? $"_t_['{Type.Name}']._wst_(_s_).getDefaultValue()"
                : $"_t_['{Type.Name}'].getDefaultValue()";
    }

}