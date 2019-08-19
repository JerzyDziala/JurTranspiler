using System;
using System.Collections.Immutable;
using System.Diagnostics;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.semantic_model {

	[DebuggerDisplay("Name = {" + nameof(Name) + "}, Type = {Type.Name}")]
	public class Field : IEquatable<Field>, ITreeNode {

		public ImmutableArray<ITreeNode> ImmediateChildren { get; }
		public ImmutableArray<ITreeNode> AllChildren { get; }

		public string OriginalFile { get; }
		public int OriginalLine { get; }
		public string Name { get; }
		public FieldDeclarationSyntax OriginalSyntax { get; }
		public IType Type { get; }
		public StructDefinitionSyntax OriginalOwnerSyntax { get; }


		public Field(FieldDeclarationSyntax originalSyntax, StructDefinitionSyntax originalOwnerSyntax, IType type) {
			Name = originalSyntax.Name;
			Type = type;
			OriginalSyntax = originalSyntax;
			OriginalOwnerSyntax = originalOwnerSyntax;
			OriginalFile = originalSyntax.File;
			OriginalLine = originalSyntax.Line;
			ImmediateChildren = ImmutableArray.Create<ITreeNode>();
			AllChildren = ImmutableArray.Create<ITreeNode>();
		}


		public bool Equals(Field other) {
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Name, other.Name) && Equals(Type, other.Type);
		}


		public override bool Equals(object? obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((Field) obj);
		}


		public override int GetHashCode() {
			unchecked {
				return (Name.GetHashCode() * 397) ^ Type.GetHashCode() * 396;
			}
		}

	}

}