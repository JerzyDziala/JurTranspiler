using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.src.syntax_tree.types;
using UtilityLibrary;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.nodes {

	public class ConstructorSyntax : IExpressionSyntax, ISyntaxNode {

		public override ISyntaxNode Root { get; }
		public override ISyntaxNode Parent { get; }
		public override ImmutableList<ISyntaxNode> AllParents { get; }
		public override ImmutableList<ITreeNode> ImmediateChildren { get; }
		public override ImmutableList<ITreeNode> AllChildren { get; }
		public override string File { get; }
		public override int Line { get; }
		public override int Abstraction { get; }

		public ITypeSyntax ConstructedType { get; }


		public ConstructorSyntax(ISyntaxNode parent, JurParser.ConstructorContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;

			ConstructedType = TypeSyntaxFactory.Create(this, context.type());
			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .Add(ConstructedType);
			AllChildren = this.GetAllChildren();

		}


		public override Type Evaluate(HashSet<Error> errors, Binder binder) => binder.BindType(ConstructedType, errors);


		public override string ToJs(Binder binder) {
			if (ConstructedType is FunctionPointerTypeSyntax pointer) {
				return $"({pointer.Parameters.Select((syntax, i) => $"p{i}").Glue(", ")}) => {pointer.ReturnType.DefaultValue};}}";
			}
			if (ConstructedType is ArrayTypeSyntax array) {
				var arrayType = binder.BindType(array, new HashSet<Error>());
				return $"Array$({((ArrayType) arrayType).ElementType.GetJsTypeCacheGetter()})";
			}
			if (ConstructedType is PrimitiveTypeSyntax primitive) {
				return primitive.DefaultValue;
			}
			if (ConstructedType is StructTypeSyntax structType) {
				var type = (StructType) binder.BindType(structType, new HashSet<Error>());
				var typeJs = type.GetJsTypeCacheGetter();
				return $"{structType.Name}({typeJs})";
			}
			else throw new Exception("invalid constructor type");
		}

	}

}