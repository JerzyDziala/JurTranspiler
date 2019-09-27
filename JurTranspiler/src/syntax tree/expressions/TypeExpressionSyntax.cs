using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

	public class TypeExpressionSyntax : ExpressionSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
		public ITypeSyntax Type { get; }


		public TypeExpressionSyntax(ISyntaxNode parent, JurParser.TypeExpressionContext context) : base(parent, context, context.TYPE()) {

			Type = ToType(context.type());

			ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(Type);

		}


		public override string ToJs(Knowledge knowledge) {
			return AllParents.Any(x => x is FunctionDefinitionSyntax function && function.IsGeneric)
				       ? $"_t_['{Type.FullName}']._wst_(_s_)"
				       : $"_t_['{Type.FullName}']";
		}

	}

}