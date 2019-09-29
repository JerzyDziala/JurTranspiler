using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.Analysis.errors.bases;
using JurTranspiler.semantic_model;
using JurTranspiler.semantic_model.functions;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.expressions;
using UtilityLibrary;

namespace JurTranspiler.Analysis.Binder {

    public partial class Binder {

        private FunctionCallInfo CheckConstraintsAndReturn(OverloadCompatibility compatibility, CallLocation location) {
            if (RespectsAllConstraints(compatibility.Substitutions)) {
                return new FunctionCallInfo(AfterSubstitution(compatibility), compatibility.Substitutions);
            }

            errors.Add(new CallArgumentsViolateParametersConstraints(location.File, location.Line, location.CallString));
            return new FunctionCallInfo(new ErrorSignature(new UndefinedType()), ImmutableHashSet<Substitution>.Empty);
        }


        private IEnumerable<OverloadCompatibility> GetOverloadsCompatibilityInfo(IReadOnlyList<IType> explicitTypeArguments,
                                                                                 IReadOnlyList<IType> argumentsTypes,
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

            Func<Func<IType, IType, bool>, bool> AllParamArgPairs = predicate => _.TrueForAllPairs(callableWithSubstitutions.ParametersTypes, overloadCompatibility.argumentTypes, predicate);
            Func<IType, IType, bool> arePerfectlyCompatible = (param, arg) => param.Equals(arg);

            return AllParamArgPairs(arePerfectlyCompatible);
        }


        private OverloadCompatibility GetOverloadCompatibility(IEnumerable<IType> explicitTypeArguments,
                                                               IReadOnlyList<IType> argumentsTypes,
                                                               bool isPoly,
                                                               Func<IType, IType, HashSet<Substitution>, bool> checkParamArgCompileTimeCompatibilityAndSubstitute,
                                                               Func<IType, IType, bool> checkParamArgRuntimeCompatibility,
                                                               Callable overload) {

            var substitutions = new HashSet<Substitution>(GetSubstitutionsFromExplicitTypeArguments(explicitTypeArguments, overload));

            //partial applications
            Func<Func<IType, IType, bool>, bool> AllParamArgPairs = predicate => _.TrueForAllPairs(overload.ParametersTypes, argumentsTypes, predicate);
            Func<IType, IType, bool> checkCompileTimeCompatibility = (param, arg) => checkParamArgCompileTimeCompatibilityAndSubstitute(param, arg, substitutions);

            return new OverloadCompatibility(callable: overload,
                                             substitutions: substitutions,
                                             allArgumentsAreCompatible: isPoly ? AllParamArgPairs(checkParamArgRuntimeCompatibility) : AllParamArgPairs(checkCompileTimeCompatibility),
                                             allTypeParametersHaveOneSubstitution: overload.TypeParameters.All(t => substitutions.One(sub => sub.typeParameter.Equals(t))),
                                             argumentsTypes);
        }


        private IEnumerable<Substitution> GetSubstitutionsFromExplicitTypeArguments(IEnumerable<IType> explicitTypeArguments, Callable overload) {
            var typeArguments = explicitTypeArguments.ToList();
            var limit = typeArguments.Count < overload.TypeParameters.Length
                            ? typeArguments.Count
                            : overload.TypeParameters.Length;

            var subs = new HashSet<Substitution>();
            for (var i = 0; i < limit; i++) {
                subs.Add(new Substitution((TypeParameterType) overload.TypeParameters[i], typeArguments[i]));
            }

            return subs;
        }


        private List<Callable> GetOverloadsFor(FunctionCallSyntax syntax) {
            var potentialFunctions = symbols.GetVisibleDefinitionsFor(syntax)
                                            .Select(BindFunctionDefinition);

            var potentialFunctionPointers = syntax.GetVisibleVariablesInScope()
                                                  .Where(variable => BindVariableType(variable) is FunctionPointerType pointerType
                                                                  && pointerType.Parameters.Length == syntax.Arguments.Length
                                                                  && variable.Name == syntax.Name)
                                                  .Select(pointerVariable => {
                                                      var pointerType = (FunctionPointerType) BindVariableType(pointerVariable);
                                                      return new FunctionPointer(declaration: pointerVariable,
                                                                                 name: pointerVariable.Name,
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


        private FunctionCallInfo CouldNotFindMatchingOverload(CallLocation location) {
            errors.Add(new NoMatchingOverloadForCall(location.File, location.Line, location.CallString));
            return new FunctionCallInfo(new ErrorSignature(new UndefinedType()), ImmutableHashSet<Substitution>.Empty);
        }


        private FunctionCallInfo AmbiguousFunctionCall(CallLocation location) {
            errors.Add(new CouldNotResolveAmbiguousFunctionCall(location.File, location.Line, location.CallString));
            return new FunctionCallInfo(new ErrorSignature(new UndefinedType()), ImmutableHashSet<Substitution>.Empty);
        }

    }

}