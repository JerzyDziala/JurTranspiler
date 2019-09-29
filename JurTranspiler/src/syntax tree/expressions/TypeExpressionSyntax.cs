using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.declarations;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.expressions {

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