using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.src.syntax_tree.types;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

	public class StructDefinitionSyntax : ISyntaxNode, IStructOrFunctionDeclarationSyntax, IEquatable<StructDefinitionSyntax> {

		//INode
		public ISyntaxNode Root { get; }
		public ISyntaxNode Parent { get; }
		public ImmutableList<ISyntaxNode> AllParents { get; }
		public ImmutableList<ITreeNode> ImmediateChildren { get; }
		public ImmutableList<ITreeNode> AllChildren { get; }
		public string File { get; }
		public int Line { get; }
		public int Abstraction { get; }

		public string Name { get; }
		public string FullName { get; }
		public bool IsGeneric { get; }
		public int Arity { get; }
		public bool IsExtern { get; }
		public ImmutableList<TypeParameterSyntax> TypeParameters { get; }
		public ImmutableList<ITypeSyntax> InlinedTypes { get; }
		public ImmutableList<FieldDeclarationSyntax> Fields { get; }


		public StructDefinitionSyntax(ISyntaxNode parent, JurParser.StructDeclarationContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = context.Start.Line;

			Name = context.ID(0).GetText();
			IsExtern = context.EXTERN() != null;
			IsGeneric = context.LESS() != null;
			TypeParameters = context.ID().Skip(1).Select(x => new TypeParameterSyntax(this, x.GetText(), Line, this)).ToImmutableList();
			InlinedTypes = context.inlinedType().Select(x => TypeSyntaxFactory.Create(this, x.type())).ToImmutableList();
			Fields = context.uninitializedVarDeclaration().Select(x => new FieldDeclarationSyntax(this, x)).ToImmutableList();
			Arity = TypeParameters.Count;

			FullName = IsGeneric
				           ? Name + "<" + string.Join(",", TypeParameters.Select(x => x.Name)) + ">"
				           : Name;

			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .AddRange(InlinedTypes)
			                                 .AddRange(Fields);

			AllChildren = this.GetAllChildren();

		}


		public string ToJs(Binder binder) {
			return $@"
function {Name}(type) {{
	let result = {{type$: type}};
	result.type$.fields.forEach(x=>result[x.name] = getDefaultValueOfType(x.type));
	return result;
}}
";
		}


		public bool Equals(StructDefinitionSyntax other) {
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(File, other.File) && Line == other.Line && string.Equals(FullName, other.FullName);
		}


		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((StructDefinitionSyntax) obj);
		}


		public override int GetHashCode() {
			unchecked {
				var hashCode = (File != null ? File.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ Line;
				hashCode = (hashCode * 397) ^ (FullName != null ? FullName.GetHashCode() : 0);
				return hashCode;
			}
		}

	}

}