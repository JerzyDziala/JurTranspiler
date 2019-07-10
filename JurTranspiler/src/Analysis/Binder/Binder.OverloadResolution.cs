using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.compilerSource.semantic_model.functions;
using UtilityLibrary;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {

        private Callable CheckConstraintsAndReturn(OverloadCompatibility compatibility, CallLocation location, HashSet<Error> errors) {
            if (RespectsAllConstraints(compatibility.Substitutions, errors)) {
                return AfterSubstitution(compatibility);
            }

            errors.Add(new CallArgumentsViolateParametersConstraints(location.File, location.Line, location.CallString));
            return new ErrorSignature(new UndefinedType());
        }


        private IEnumerable<OverloadCompatibility> GetOverloadsCompatibilityInfo(IReadOnlyList<Type> explicitTypeArguments,
                                                                                 IReadOnlyList<Type> argumentsTypes,
                                                                                 IEnumerable<Callable> overloads,
                                                                                 bool isPoly,
                                                                                 HashSet<Error> errors) {

            return overloads.Select(overload => GetOverloadCompatibility(explicitTypeArguments: explicitTypeArguments,
                                                                         argumentsTypes: argumentsTypes,
                                                                         isPoly: isPoly,
                                                                         checkParamArgCompileTimeCompatibilityAndSubstitute: (par, arg, subs) => arg.IsAssignableToWithSubstitutions(par, subs, errors),
                                                                         checkParamArgRuntimeCompatibility: (par, arg) => symbols.TypesBindings.Values.Any(t => t.IsAssignableTo(arg, errors) && t.IsAssignableTo(par, errors)),
                                                                         overload: overload));
        }


        private bool IsPerfectCompatibility(OverloadCompatibility overloadCompatibility) {

            var callableWithSubstitutions = AfterSubstitution(overloadCompatibility);

            Func<Func<Type, Type, bool>, bool> AllParamArgPairs = predicate => _.TrueForAllPairs(callableWithSubstitutions.ParametersTypes, overloadCompatibility.argumentTypes, predicate);
            Func<Type, Type, bool> arePerfectlyCompatible = (param, arg) => param.Equals(arg);

            return AllParamArgPairs(arePerfectlyCompatible);
        }


        private OverloadCompatibility GetOverloadCompatibility(IEnumerable<Type> explicitTypeArguments,
                                                               IReadOnlyList<Type> argumentsTypes,
                                                               bool isPoly,
                                                               Func<Type, Type, HashSet<Substitution>, bool> checkParamArgCompileTimeCompatibilityAndSubstitute,
                                                               Func<Type, Type, bool> checkParamArgRuntimeCompatibility,
                                                               Callable overload) {

            var substitutions = new HashSet<Substitution>(GetSubstitutionsFromExplicitTypeArguments(explicitTypeArguments, overload));

            //partial applications
            Func<Func<Type, Type, bool>, bool> AllParamArgPairs = predicate => _.TrueForAllPairs(overload.ParametersTypes, argumentsTypes, predicate);
            Func<Type, Type, bool> checkCompileTimeCompatibility = (param, arg) => checkParamArgCompileTimeCompatibilityAndSubstitute(param, arg, substitutions);

            return new OverloadCompatibility(callable: overload,
                                             substitutions: substitutions,
                                             allArgumentsAreCompatible: isPoly ? AllParamArgPairs(checkParamArgRuntimeCompatibility) : AllParamArgPairs(checkCompileTimeCompatibility),
                                             allTypeParametersHaveOneSubstitution: overload.TypeParameters.All(t => substitutions.One(sub => sub.typeParameter.Equals(t))),
                                             argumentsTypes);
        }


        private IEnumerable<Substitution> GetSubstitutionsFromExplicitTypeArguments(IEnumerable<Type> explicitTypeArguments, Callable overload) {
            var typeArguments = explicitTypeArguments.ToList();
            var limit = typeArguments.Count < overload.TypeParameters.Count
                            ? typeArguments.Count
                            : overload.TypeParameters.Count;

            var subs = new HashSet<Substitution>();
            for (int i = 0; i < limit; i++) {
                subs.Add(new Substitution((TypeParameterType) overload.TypeParameters[i], typeArguments[i]));
            }

            return subs;
        }


        private List<Callable> GetOverloadsFor(FunctionCallSyntax syntax, HashSet<Error> errors) {
            var potentialFunctions = symbols.GetVisibleDefinitionsFor(syntax)
                                            .Select(x => BindFunctionDefinition(x, errors));

            var potentialFunctionPointers = symbols.GetVisibleVariablesInScope(syntax)
                                                   .Where(variable => BindVariableType(variable, errors) is FunctionPointerType pointerType
                                                                   && pointerType.Parameters.Count == syntax.Arguments.Count)
                                                   .Select(pointerVariable => {
                                                       var pointerType = (FunctionPointerType) BindVariableType(pointerVariable, errors);
                                                       return new FunctionPointer(name: pointerVariable.Name,
                                                                                  parametersTypes: pointerType.Parameters,
                                                                                  returnType: pointerType.ReturnType);
                                                   });

            return potentialFunctions.Concat<Callable>(potentialFunctionPointers).ToList();
        }


        private bool RespectsAllConstraints(ImmutableHashSet<Substitution> substitutions, HashSet<Error> errors) {
            return substitutions.All(substitution => substitution.typeParameter.GetAllConstraints().All(constraint => substitution.typeArgument.IsAssignableTo(substitutions.First(sub => sub.typeParameter.Equals(constraint)).typeArgument, errors)));
        }


        private Callable AfterSubstitution(OverloadCompatibility overload) {
            return overload.Callable is FunctionSignature signature
                       ? signature.WithSubstitutedTypes(overload.Substitutions)
                       : overload.Callable;
        }


        private ICallable CouldNotFindMatchingOverload(HashSet<Error> errors, CallLocation location) {
            errors.Add(new NoMatchingOverloadForCall(location.File, location.Line, location.CallString));
            return new ErrorSignature(new UndefinedType());
        }


        private ICallable AmbiguousFunctionCall(HashSet<Error> errors, CallLocation location) {
            errors.Add(new CouldNotResolveAmbiguousFunctionCall(location.File, location.Line, location.CallString));
            return new ErrorSignature(new UndefinedType());
        }

    }

}