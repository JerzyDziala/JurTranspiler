using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.semantic_model.functions {

	public class FunctionSignature : Callable {

		public FunctionDefinitionSyntax OriginalDefinition { get; }
		public override string Name { get; }
		public override ImmutableList<Type> ParametersTypes { get; }
		public override ImmutableList<Type> TypeParameters { get; }
		public override int GenericArity { get; }
		public override int Arity { get; }
		public bool IsGeneric { get; }
		public bool IsExtern { get; }
		public override Type ReturnType { get; }
		public int Abstraction { get; }


		public FunctionSignature(FunctionDefinitionSyntax originalDefinitionSyntax,
		                         IEnumerable<Type> typeArguments,
		                         IEnumerable<Type> parameters,
		                         Type returnType) {
			OriginalDefinition = originalDefinitionSyntax;
			IsExtern = originalDefinitionSyntax.IsExtern;
			Name = originalDefinitionSyntax.Name;
			ReturnType = returnType;
			ParametersTypes = parameters.ToImmutableList();
			TypeParameters = typeArguments.ToImmutableList();
			GenericArity = TypeParameters.Count;
			Arity = parameters.Count();
			IsGeneric = GenericArity > 0;
			Abstraction = originalDefinitionSyntax.Abstraction;
		}


		public FunctionSignature WithSubstitutedTypes(ISet<Substitution> typeMap) {
			return new FunctionSignature(originalDefinitionSyntax: OriginalDefinition,
			                             typeArguments: typeMap.Select(substitution => substitution.typeArgument),
			                             parameters: ParametersTypes.Select(p => p.WithSubstitutedTypes(typeMap)),
			                             returnType: ReturnType.WithSubstitutedTypes(typeMap));
		}


		protected bool Equals(FunctionSignature other) {
			return Equals(OriginalDefinition, other.OriginalDefinition);
		}


		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((FunctionSignature) obj);
		}


		public override int GetHashCode() {
			return (OriginalDefinition != null ? OriginalDefinition.GetHashCode() : 0);
		}

	}

}