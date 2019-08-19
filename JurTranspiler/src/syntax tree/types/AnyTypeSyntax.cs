using System;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.syntax_tree.types {

	public class AnyTypeSyntax : SyntaxNode, ITypeSyntax, IEquatable<AnyTypeSyntax> {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public string Name => "any";
		public string FullName => Name;


		public AnyTypeSyntax(ISyntaxNode parent, JurParser.AnyTypeContext context) : base(parent, context) {
			ImmediateChildren = ImmutableArray.Create<ITreeNode>();
		}


		public override string ToJs(Knowledge knowledge) => throw new NotImplementedException();


		public bool Equals(AnyTypeSyntax other) {
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Name, other.Name);
		}


		public override bool Equals(object? obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((AnyTypeSyntax) obj);
		}


		public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);

	}

}