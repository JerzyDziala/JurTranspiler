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

		public FunctionSignature BindFunctionDefinition(FunctionDefinitionSyntax syntax) {
			return symbols.AlreadyBound(syntax)
				       ? symbols.GetBindingFor(syntax)
				       : symbols.MakeBindingFor(syntax, BindFunctionDefinitionCore(syntax));
		}


		private FunctionSignature BindFunctionDefinitionCore(FunctionDefinitionSyntax syntax) {

			//check for duplicate type arguments
			var typeArgumentsGroups = syntax.TypeParameters.GetDuplicates(x => x.FullName);
			foreach (var typeArgumentsGroup in typeArgumentsGroups) {
				errors.Add(new MultipleTypeParametersWithTheSameName(typeArgumentsGroup.Select(x => (x.File, x.Line)), typeArgumentsGroup.First().FullName));
			}

			//check for use of nonTypeParameters in constraints
			var invalidTypesUsedInConstraints = syntax.Constraints.SelectMany(x => new[] { x.constrained, x.constrain }).Where(x => !(x is TypeParameterSyntax));

			foreach (var invalid in invalidTypesUsedInConstraints) {
				errors.Add(new UseOfNonTypeParameterInConstraint(file: invalid.File,
				                                                 line: invalid.Line,
				                                                 name: invalid.FullName));
			}

			var returnType = BindType(syntax.ReturnType);
			return new FunctionSignature(originalDefinitionSyntax: syntax,
			                             typeArguments: syntax.TypeParameters.Distinct().Select(BindType).ToImmutableArray(),
			                             parameters: syntax.Parameters.Select(x => BindType(x.Type!)).ToImmutableArray(),
			                             returnType: returnType);
		}


		public FunctionCallInfo BindFunctionCall(FunctionCallSyntax call) {
			return symbols.AlreadyBound(call)
				       ? symbols.GetBindingFor(call)
				       : symbols.MakeBindingFor(call, BindFunctionCallCore(call));
		}


		public FunctionCallInfo BindFunctionCallCore(FunctionCallSyntax call) {

			var argumentsTypes = call.Arguments.Select(BindExpression).ToList();
			var explicitTypeArgumentsTypes = call.ExplicitTypeArguments.Select(BindType).ToList();
			var overloads = GetOverloadsFor(call);

			return BindFunctionCallCore(overloads: overloads,
			                            argumentsTypes: argumentsTypes,
			                            explicitTypeArguments: explicitTypeArgumentsTypes,
			                            isPoly: call.IsPoly,
			                            location: new CallLocation(call.File, call.Line, GetCallString()));

			string GetCallString() {
				var explicitList = call.HasExplicitTypeArguments ? $"<{string.Join(",", call.ExplicitTypeArguments.Select(x => x.FullName))}>" : "";
				return $"{call.Name}{explicitList}({string.Join(",", argumentsTypes.Select(x => x.Name))})";
			}
		}


		public FunctionCallInfo BindFunctionCallCore(IReadOnlyList<Callable> overloads,
		                                             IReadOnlyList<IType> argumentsTypes,
		                                             IReadOnlyList<IType> explicitTypeArguments,
		                                             bool isPoly,
		                                             CallLocation location) {

			//check for use of undefined function/overload
			if (overloads.None()) {
				errors.Add(new UseOfUndeclaredFunction(location.File, location.Line, location.CallString));
				return new FunctionCallInfo(new ErrorSignature(new UndefinedType()), ImmutableHashSet<Substitution>.Empty);
			}

			//check for calls that use undefined arguments
			if (argumentsTypes.Any(x => x is UndefinedType) || explicitTypeArguments.Any(x => x is UndefinedType)) {
				return new FunctionCallInfo(new ErrorSignature(new UndefinedType()), ImmutableHashSet<Substitution>.Empty);
			}

			if (isPoly) {
				var overloadsInfo = getCompatibility(true);
				var possibleAtRuntime = overloadsInfo.Where(x => x.IsCompatible()).Select(x => new FunctionCallInfo(AfterSubstitution(x), x.Substitutions)).ToList();

				if (possibleAtRuntime.One()) return possibleAtRuntime.First();
				if (possibleAtRuntime.None()) return CouldNotFindMatchingOverload(location);
				return getDispatcherOrErrorSignature();

				FunctionCallInfo getDispatcherOrErrorSignature() {
					return possibleAtRuntime.AllHaveSame(x => x.Callable.ReturnType)
						       ? new FunctionCallInfo(new Dispatcher(possibleAtRuntime.ToImmutableArray()), ImmutableHashSet<Substitution>.Empty)
						       : new FunctionCallInfo(new ErrorSignature(new UndefinedType()), ImmutableHashSet<Substitution>.Empty);
				}
			}
			else {
				var overloadsInfo = getCompatibility(false);
				var compatibleOverloads = overloadsInfo.Where(x => x.IsCompatible()).ToList();

				if (compatibleOverloads.One()) return checkConstraintsAndReturn(compatibleOverloads.First());
				if (compatibleOverloads.None()) return CouldNotFindMatchingOverload(location);
				return resolve();

				FunctionCallInfo resolve() {
					var perfectMatches = compatibleOverloads.Where(IsPerfectCompatibility).ToImmutableArray();
					return perfectMatches.One()
						       ? checkConstraintsAndReturn(perfectMatches.First())
						       : AmbiguousFunctionCall(location);
				}
			}

			FunctionCallInfo checkConstraintsAndReturn(OverloadCompatibility x) => CheckConstraintsAndReturn(x, location);

			IEnumerable<OverloadCompatibility> getCompatibility(bool poly) => GetOverloadsCompatibilityInfo(explicitTypeArguments: explicitTypeArguments,
			                                                                                                argumentsTypes: argumentsTypes,
			                                                                                                overloads: overloads,
			                                                                                                isPoly: poly);

		}

	}

}