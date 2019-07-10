using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.compilerSource.semantic_model.functions;
using JurTranspiler.src.syntax_tree.types;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {

        public FunctionSignature BindFunctionDefinition(FunctionDefinitionSyntax syntax, HashSet<Error> errors) {
            return symbols.AlreadyBound(syntax)
                       ? symbols.GetBindingFor(syntax)
                       : symbols.MakeBindingFor(syntax, BindFunctionDefinitionCore(syntax, errors));
        }


        private FunctionSignature BindFunctionDefinitionCore(FunctionDefinitionSyntax syntax, HashSet<Error> errors) {

            //check for duplicate type arguments
            var typeArgumentsGroups = syntax.TypeParametersInGenericTypesList.GetDuplicates(x => x.FullName);
            foreach (var typeArgumentsGroup in typeArgumentsGroups) {
                errors.Add(new MultipleTypeParametersWithTheSameName(typeArgumentsGroup.Select(x => (x.File, x.Line)), typeArgumentsGroup.First().FullName));
            }

            //check for use of nonTypeParameters in constraints
            var invalidTypesUsedInConstraints = syntax.Constraints.SelectMany(x => new[] {x.constrained, x.constrain}).Where(x => !(x is TypeParameterSyntax));

            foreach (var invalid in invalidTypesUsedInConstraints) {
                errors.Add(new UseOfNonTypeParameterInConstraint(file: invalid.File,
                                                                 line: invalid.Line,
                                                                 name: invalid.FullName));
            }

            return new FunctionSignature(originalDefinitionSyntax: syntax,
                                         typeArguments: syntax.TypeParametersInGenericTypesList.Distinct().Select(x => BindType(x, errors)),
                                         parameters: syntax.Parameters.Select(x => BindType(x.Type, errors)),
                                         returnType: BindType(syntax.ReturnType, errors));
        }


        public ICallable BindFunctionCall(FunctionCallSyntax call, HashSet<Error> errors) {
            return symbols.AlreadyBound(call)
                       ? symbols.GetBindingFor(call)
                       : symbols.MakeBindingFor(call, BindFunctionCallCore(call, errors));
        }


        public ICallable BindFunctionCallCore(FunctionCallSyntax call, HashSet<Error> errors) {

            var argumentsTypes = call.Arguments.Select(a => a.Evaluate(errors, this)).ToList();
            var explicitTypeArgumentsTypes = call.ExplicitTypeArguments.Select(typeSyntax => BindType(typeSyntax, errors)).ToList();
            var overloads = GetOverloadsFor(call, errors);
            return BindFunctionCallCore(overloads: overloads,
                                        argumentsTypes: argumentsTypes,
                                        explicitTypeArguments: explicitTypeArgumentsTypes,
                                        isPoly: call.IsPoly,
                                        location: new CallLocation(call.File, call.Line, call.GetCallString(errors, this)),
                                        errors: errors);
        }


        public ICallable BindFunctionCallCore(IReadOnlyList<Callable> overloads,
                                              IReadOnlyList<Type> argumentsTypes,
                                              IReadOnlyList<Type> explicitTypeArguments,
                                              bool isPoly,
                                              CallLocation location,
                                              HashSet<Error> errors) {

            //check for use of undefined function/overload
            if (overloads.None()) {
                errors.Add(new UseOfUndeclaredFunction(location.File, location.Line, location.CallString));
                return new ErrorSignature(new UndefinedType());
            }

            //check for calls that use undefined arguments
            if (argumentsTypes.Any(x => x is UndefinedType) || explicitTypeArguments.Any(x => x is UndefinedType)) {
                return new ErrorSignature(new UndefinedType());
            }

            if (isPoly) {
                var overloadsInfo = getCompatibility(true);
                var possibleAtRuntime = overloadsInfo.Where(x => x.IsCompatible()).Select(AfterSubstitution).ToList();

                if (possibleAtRuntime.One()) return possibleAtRuntime.First();
                if (possibleAtRuntime.None()) return CouldNotFindMatchingOverload(errors, location);
                return getDispatcherOrErrorSignature();

                ICallable getDispatcherOrErrorSignature() => possibleAtRuntime.AllHaveSame(x => x.ReturnType)
                                                                 ? new Dispatcher(possibleAtRuntime.ToImmutableList())
                                                                 : (ICallable) new ErrorSignature(new UndefinedType());
            }
            else {
                var overloadsInfo = getCompatibility(false);
                var compatibleOverloads = overloadsInfo.Where(x => x.IsCompatible()).ToList();

                if (compatibleOverloads.One()) return checkConstraintsAndReturn(compatibleOverloads.First());
                if (compatibleOverloads.None()) return CouldNotFindMatchingOverload(errors, location);
                return resolve();

                ICallable resolve() {
                    var perfectMatches = compatibleOverloads.Where(IsPerfectCompatibility).ToImmutableList();
                    return perfectMatches.One()
                               ? checkConstraintsAndReturn(perfectMatches.First())
                               : AmbiguousFunctionCall(errors, location);
                }
            }

            ICallable checkConstraintsAndReturn(OverloadCompatibility x) => CheckConstraintsAndReturn(x, location, errors);

            IEnumerable<OverloadCompatibility> getCompatibility(bool poly) => GetOverloadsCompatibilityInfo(explicitTypeArguments: explicitTypeArguments,
                                                                                                            argumentsTypes: argumentsTypes,
                                                                                                            overloads: overloads,
                                                                                                            isPoly: poly,
                                                                                                            errors: errors);

        }

    }

}