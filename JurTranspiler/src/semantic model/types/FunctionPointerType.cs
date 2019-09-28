using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using JurTranspiler.compilerSource.nodes;
using Type = JurTranspiler.syntax_tree.bases.Type;

namespace JurTranspiler.compilerSource.semantic_model {

	[DebuggerDisplay("{" + nameof(Name) + "}")]
	public class FunctionPointerType : Type, IEquatable<FunctionPointerType> {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public override string Name { get; }

		public IType ReturnType { get; }
		public ImmutableArray<IType> Parameters { get; }
		public int Arity => Parameters.Length;


		public FunctionPointerType(IType returnType, IEnumerable<IType> parameters) {
			ReturnType = returnType;
			Parameters = parameters.ToImmutableArray();
			Name = ReturnType.Name + "(" + string.Join(",", Parameters.Select(x => x.Name)) + ")";

			ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(ReturnType).AddRange(Parameters);

		}


		public override IType WithSubstitutedTypes(ISet<Substitution> typeMap) {
			return new FunctionPointerType(ReturnType.WithSubstitutedTypes(typeMap),
			                               Parameters.Select(x => x.WithSubstitutedTypes(typeMap)).ToImmutableArray());
		}


		public bool Equals(FunctionPointerType other) {
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Name, other.Name) && Equals(ReturnType, other.ReturnType) && Parameters.SequenceEqual(other.Parameters);
		}


		public override bool Equals(object? obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj?.GetType() != GetType()) return false;
			return Equals((FunctionPointerType) obj!);
		}


		public override int GetHashCode() {
			unchecked {
				var hashCode = (Name != null ? Name.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (ReturnType != null ? ReturnType.GetHashCode() : 0);
				foreach (var parameter in Parameters) {
					hashCode = (hashCode * 397) ^ parameter.GetHashCode();
				}
				return hashCode;
			}
		}

	}

}