using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.semantic_model {

	public class PrimitiveType : Type, IEquatable<PrimitiveType> {

		public override ImmutableList<ITreeNode> ImmediateChildren { get; }
		public override ImmutableList<ITreeNode> AllChildren { get; }
		public override string Name { get; }

		public PrimitiveKind PrimitiveKind { get; }


		public PrimitiveType(PrimitiveKind primitiveKind) {
			PrimitiveKind = primitiveKind;
			Name = PrimitiveKind.ToString().ToLower();
			ImmediateChildren = ImmutableList.Create<ITreeNode>();
			AllChildren = this.GetAllChildren();
		}


		protected override bool IsAssignableToCore(Type type, HashSet<Error> errors) {
			if (type is UndefinedType) return true;
			if (type is PrimitiveType p) {
				if (PrimitiveKind == p.PrimitiveKind) return true;
			}
			return false;
		}


		public override bool IsAssignableToWithSubstitutions(Type type, ICollection<Substitution> substitutions, HashSet<Error> errors) {
			if (IsAssignableTo(type, errors)) return true;
			if (type is TypeParameterType t) {
				substitutions.Add(new Substitution(t, this));
				return true;
			}
			return false;
		}


		public override bool IsEqualToWithSubstitutions(Type type, ICollection<Substitution> substitutions, HashSet<Error> errors) {
			if (type is TypeParameterType t) {
				substitutions.Add(new Substitution(t, this));
				return true;
			}
			return Equals(type);
		}


		public override Type WithSubstitutedTypes(ISet<Substitution> typeMap) => this;


		public override string GetJsTypeCacheGetter() {
			return $"cachedTypes$['{Name}']";
		}


		public override string GetDefaultValue() {
			switch (PrimitiveKind) {
				case PrimitiveKind.STRING: return "''";
				case PrimitiveKind.BOOL:   return "false";
				case PrimitiveKind.NUM:    return "0";
				default:                   throw new ArgumentOutOfRangeException();
			}
		}


		public bool Equals(PrimitiveType other) {
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Name, other.Name) && PrimitiveKind == other.PrimitiveKind;
		}


		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((PrimitiveType) obj);
		}


		public override int GetHashCode() {
			unchecked {
				return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (int) PrimitiveKind;
			}
		}

	}


	public enum PrimitiveKind {

		STRING,
		BOOL,
		NUM,

	}

}