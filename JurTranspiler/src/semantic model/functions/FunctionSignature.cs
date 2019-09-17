using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.semantic_model.functions {

	public class FunctionSignature : Callable {

		public FunctionDefinitionSyntax OriginalDefinition { get; }
		public override bool IsStatic => OriginalDefinition.IsStatic;
		public override bool IsExtern => OriginalDefinition.IsExtern;
		public override bool IsMember => OriginalDefinition.IsMember;
		public override string? StaticTypeName => OriginalDefinition.StaticTypeName;


		public FunctionSignature(FunctionDefinitionSyntax originalDefinitionSyntax,
		                         ImmutableArray<IType> typeArguments,
		                         ImmutableArray<IType> parameters,
		                         IType returnType) : base(originalDefinitionSyntax.Name,
		                                                  parameters,
		                                                  typeArguments,
		                                                  returnType) {
			OriginalDefinition = originalDefinitionSyntax;
		}


		public FunctionSignature WithSubstitutedTypes(ISet<Substitution> typeMap) {
			return new FunctionSignature(originalDefinitionSyntax: OriginalDefinition,
			                             typeArguments: typeMap.Select(substitution => substitution.typeArgument).ToImmutableArray(),
			                             parameters: ParametersTypes.Select(p => p.WithSubstitutedTypes(typeMap)).ToImmutableArray(),
			                             returnType: ReturnType.WithSubstitutedTypes(typeMap));
		}


		protected bool Equals(FunctionSignature other) {
			return Equals(OriginalDefinition, other.OriginalDefinition);
		}


		public override bool Equals(object? obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj?.GetType() != GetType()) return false;
			return Equals((FunctionSignature) obj!);
		}


		public override int GetHashCode() {
			return OriginalDefinition?.GetHashCode() ?? 0;
		}

	}

}