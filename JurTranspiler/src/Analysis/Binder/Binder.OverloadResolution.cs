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

        private Callable CheckConstraintsAndReturn(OverloadCompatibility compatibility, CallLocation location) {
            if (RespectsAllConstraints(compatibility.Substitutions)) {
                return AfterSubstitution(compatibility);
            }

            errors.Add(new CallArgumentsViolateParametersConstraints(location.File, location.Line, location.CallString));
            return new ErrorSignature(new UndefinedType());
        }


        private IEnumerable<OverloadCompatibility> GetOverloadsCompatibilityInfo(IReadOnlyList<Type> explicitTypeArguments,
                                                                                 IReadOnlyList<Type> argumentsTypes,
                                                                                 IEnumerable<Callable> overloads,
                                                                                 bool isPoly) {

            return overloads.Select(overload => GetOverloadCompatibility(explicitTypeArguments: explicitTypeArguments,
                                                                         argumentsTypes: argumentsTypes,
                                                                         isPoly: isPoly,
                                                                         checkParamArgCompileTimeCompatibilityAndSubstitute: (par, arg, subs) => IsAssignableToWithSubstitutions(arg, par, subs),
                                                                         checkParamArgRuntimeCompatibility: (par, arg) => symbols.TypesBindings.Values.Any(t => IsAssignableTo(t, arg) && IsAssignableTo(t, par)),
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


        private List<Callable> GetOverloadsFor(FunctionCallSyntax syntax) {
            var potentialFunctions = symbols.GetVisibleDefinitionsFor(syntax)
                                            .Select(BindFunctionDefinition);

            var potentialFunctionPointers = symbols.GetVisibleVariablesInScope(syntax)
                                                   .Where(variable => BindVariableType(variable) is FunctionPointerType pointerType
                                                                   && pointerType.Parameters.Count == syntax.Arguments.Count)
                                                   .Select(pointerVariable => {
                                                       var pointerType = (FunctionPointerType) BindVariableType(pointerVariable);
                                                       return new FunctionPointer(name: pointerVariable.Name,
                                                                                  parametersTypes: pointerType.Parameters,
                                                                                  returnType: pointerType.ReturnType);
                                                   });

            return potentialFunctions.Concat<Callable>(potentialFunctionPointers).ToList();
        }


        private bool RespectsAllConstraints(ImmutableHashSet<Substitution> substitutions) {
            return substitutions.All(sub => sub.typeParameter
                                               .GetAllConstraints()
                                               .All(constraint => IsAssignableTo(sub.typeArgument,
                                                                                 substitutions.First(s => s.typeParameter.Equals(constraint)).typeArgument)));
        }


        private Callable AfterSubstitution(OverloadCompatibility overload) {
            return overload.Callable is FunctionSignature signature
                       ? signature.WithSubstitutedTypes(overload.Substitutions)
                       : overload.Callable;
        }


        private ICallable CouldNotFindMatchingOverload(CallLocation location) {
            errors.Add(new NoMatchingOverloadForCall(location.File, location.Line, location.CallString));
            return new ErrorSignature(new UndefinedType());
        }


        private ICallable AmbiguousFunctionCall(CallLocation location) {
            errors.Add(new CouldNotResolveAmbiguousFunctionCall(location.File, location.Line, location.CallString));
            return new ErrorSignature(new UndefinedType());
        }

    }

}