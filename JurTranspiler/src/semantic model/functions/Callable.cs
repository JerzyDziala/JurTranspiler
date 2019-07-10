using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.semantic_model.functions {

    public abstract class Callable : ICallable {

        public abstract string Name { get; }

        public abstract ImmutableList<Type> ParametersTypes { get; }

        public abstract ImmutableList<Type> TypeParameters { get; }

        public abstract int GenericArity { get; }

        public abstract int Arity { get; }

        public abstract Type ReturnType { get; }


        public bool IsConsideredSameAs(Callable other) {

            if (Name == other.Name && GenericArity == other.GenericArity && ParametersTypes.Count == other.ParametersTypes.Count) {
                //they're considered same if theirs parameter types lists are the same BUT ignoring the names of the type Parameters.
                //I'm just going to take a lazy suboptimal route here and simply substitute theirs type parameters such that they're the same

                var typeMap = new HashSet<Substitution>();
                var typeArguments = other.TypeParameters.Cast<TypeParameterType>().ToList();
                for (int i = 0; i < typeArguments.Count; i++) {
                    typeMap.Add(new Substitution(typeArguments[i], TypeParameters[i]));
                }
                var otherParametersAfterSubstitution = other.ParametersTypes.Select(p => p.WithSubstitutedTypes(typeMap));

                //if there is no parameters that are different from it's counterparts then the signatures are considered equals
                if (otherParametersAfterSubstitution.WithIndexes().All(pair => pair.value.Equals(ParametersTypes[pair.i]))) return true;
            }
            return false;
        }


    }

}