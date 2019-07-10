using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.semantic_model.functions {

    public class OverloadCompatibility {

        public ImmutableList<Type> argumentTypes { get; }
        public Callable Callable { get; }
        public ImmutableHashSet<Substitution> Substitutions { get; }
        public bool AllArgumentsAreCompatible { get; }
        public bool AllTypeParametersHaveOneSubstitution { get; }


        public OverloadCompatibility(Callable callable,
                                     IEnumerable<Substitution> substitutions,
                                     bool allArgumentsAreCompatible,
                                     bool allTypeParametersHaveOneSubstitution,
                                     IEnumerable<Type> argumentTypes) {
            Callable = callable;
            Substitutions = substitutions.ToImmutableHashSet();
            AllArgumentsAreCompatible = allArgumentsAreCompatible;
            AllTypeParametersHaveOneSubstitution = allTypeParametersHaveOneSubstitution;
            this.argumentTypes = argumentTypes.ToImmutableList();
        }


        public bool IsCompatible() => AllArgumentsAreCompatible && AllTypeParametersHaveOneSubstitution;

    }

}