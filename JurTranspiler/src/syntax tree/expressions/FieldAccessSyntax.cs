using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Antlr4.Runtime;
using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

	public class FieldAccessSyntax : IExpressionSyntax, ISyntaxNode {

		//INode
		public override ISyntaxNode Root { get; }
		public override ISyntaxNode Parent { get; }
		public override ImmutableList<ISyntaxNode> AllParents { get; }
		public override ImmutableList<ITreeNode> ImmediateChildren { get; }
		public override ImmutableList<ITreeNode> AllChildren { get; }
		public override string File { get; }
		public override int Line { get; }
		public override int Abstraction { get; }

		public IExpressionSyntax Owner { get; }
		public string Name { get; }


		public FieldAccessSyntax(ISyntaxNode parent, JurParser.FieldAccessContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;

			Owner = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression());
			Name = context.ID().GetText();
			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .Add(Owner);
			AllChildren = this.GetAllChildren();
		}


		public override Type Evaluate(HashSet<Error> errors, Binder binder) {
			var ownerType = Owner.Evaluate(errors, binder);
			if (ownerType is StructType structType) {
				var fields = structType.GetAllFields(errors);
				var matchingFields = fields.Where(field => field.Name == Name).ToList();
				if (matchingFields.None()) {
					//error: no matching field
					errors.Add(new NoMatchingFieldFound(file: File,
					                                    line: Line,
					                                    fieldName: Name,
					                                    typeName: ownerType.Name));
					return new UndefinedType();
				}
				if (matchingFields.Count > 1) {
					//error: ambiguous field reference
					errors.Add(new AmbiguousFieldReference(file: File,
					                                       line: Line,
					                                       fieldName: Name,
					                                       typeName: ownerType.Name));
					return new UndefinedType();
				}
				else return matchingFields.First().Type;
			}
			return new UndefinedType();
		}


		public override string ToJs(Binder binder) {
			return $"{Owner.ToJs(binder)}.{Name}";
		}

	}

}