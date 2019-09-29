using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis;
using JurTranspiler.CodeGeneration;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.Interfaces;
using JurTranspiler.syntax_tree.types;
using UtilityLibrary;

namespace JurTranspiler.syntax_tree.expressions {

	public class ConstructorSyntax : ExpressionSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public ITypeSyntax ConstructedType { get; }
		public ImmutableArray<InitializerSyntax> Initializers { get; }
		public bool HasInitializers => Initializers.Any();


		public ConstructorSyntax(ISyntaxNode parent, JurParser.ConstructorContext context) : base(parent, context) {

			ConstructedType = ToType(context.type());
			Initializers = context.ID()
			                      .Select((node, i) => new InitializerSyntax(this, node.GetText().ToString(), context.expression(i)))
			                      .ToImmutableArray();
			ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(ConstructedType).AddRange(Initializers);

		}


		public override string ToJs(Knowledge knowledge) {
			var needsSubstitutions = ConstructedType.AllChildren.Any(x => x is TypeParameterSyntax);
			var type = "_t_[" + ConstructedType.FullName.AsString() + "]";

			if (needsSubstitutions) {
				type += "._wst_(_s_)";
			}
			var withoutInitializer = type + ".createInstance()";

			if (HasInitializers) {
				var initializers = Initializers.Select(x => $"{x.FieldName}: {x.Expression.ToJs(knowledge)}").Glue(",").AsObject();
				return $"_wi_({withoutInitializer}, {initializers})";
			}
			return withoutInitializer;
		}

	}

}