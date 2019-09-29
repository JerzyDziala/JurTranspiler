using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.semantic_model.types;

namespace JurTranspiler.semantic_model.functions {

    public class OverloadCompatibility {

        public ImmutableArray<IType> argumentTypes { get; }
        public Callable Callable { get; }
        public ImmutableHashSet<Substitution> Substitutions { get; }
        public bool AllArgumentsAreCompatible { get; }
        public bool AllTypeParametersHaveOneSubstitution { get; }


        public OverloadCompatibility(Callable callable,
                                     IEnumerable<Substitution> substitutions,
                                     bool allArgumentsAreCompatible,
                                     bool allTypeParametersHaveOneSubstitution,
                                     IEnumerable<IType> argumentTypes) {
            Callable = callable;
            Substitutions = substitutions.ToImmutableHashSet();
            AllArgumentsAreCompatible = allArgumentsAreCompatible;
            AllTypeParametersHaveOneSubstitution = allTypeParametersHaveOneSubstitution;
            this.argumentTypes = argumentTypes.ToImmutableArray();
        }


        public bool IsCompatible() => AllArgumentsAreCompatible && AllTypeParametersHaveOneSubstitution;

    }

}