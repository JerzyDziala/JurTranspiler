using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.Analysis.errors.bases;
using JurTranspiler.semantic_model;
using JurTranspiler.semantic_model.functions;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.declarations;
using JurTranspiler.syntax_tree.expressions;
using JurTranspiler.syntax_tree.types;
using UtilityLibrary;

namespace JurTranspiler.Analysis.Binder {

	public partial class Binder {
		
		private FunctionSignature BindFunctionDefinition(FunctionDefinitionSyntax syntax) {
			return symbols.AlreadyBound(syntax)
				       ? symbols.GetBindingFor(syntax)
				       : symbols.MakeBindingFor(syntax, BindFunctionDefinitionCore(syntax));
		}


		private FunctionSignature BindFunctionDefinitionCore(FunctionDefinitionSyntax syntax) {

			//check for duplicate type arguments
			var typeArgumentsGroups = syntax.TypeParameters.GetDuplicates(x => x.FullName);
			foreach (var typeArgumentsGroup in typeArgumentsGroups) {
				errors.Add(new MultipleTypeParametersWithTheSameName(typeArgumentsGroup.Select(x => new Location(x.File, x.Line)), typeArgumentsGroup.First().FullName));
			}

			//check for use of nonTypeParameters in constraints
			var invalidTypesUsedInConstraints = syntax.Constraints.SelectMany(x => new[] { x.constrained, x.constrain }).Where(x => !(x is TypeParameterSyntax));

			foreach (var invalid in invalidTypesUsedInConstraints) {
				errors.Add(new UseOfNonTypeParameterInConstraint(file: invalid.File,
				                                                 line: invalid.Line,
				                                                 name: invalid.FullName));
			}
			
			//check for 0 arguments member functions
			if (syntax.IsMember && syntax.Arity == 0) {
				errors.Add(new MemberFunctionWithoutArguments(syntax.Location, syntax.Name));
			}
				

			var returnType = BindType(syntax.ReturnType);
			return new FunctionSignature(originalDefinitionSyntax: syntax,
			                             typeArguments: syntax.TypeParameters.Distinct().Select(BindType).ToImmutableArray(),
			                             parameters: syntax.Parameters.Select(x => BindType(x.Type!)).ToImmutableArray(),
			                             returnType: returnType);
		}


		private FunctionCallInfo BindFunctionCall(FunctionCallSyntax call) {
			return symbols.AlreadyBound(call)
				       ? symbols.GetBindingFor(call)
				       : symbols.MakeBindingFor(call, BindFunctionCallCore(call));
		}


		private FunctionCallInfo BindFunctionCallCore(FunctionCallSyntax call) {

			var argumentsTypes = call.Arguments.Select(BindExpression).ToList();
			var explicitTypeArgumentsTypes = call.ExplicitTypeArguments.Select(BindType).ToList();

			//if first argument is a Struct then this call may be a field function pointer call
			if (explicitTypeArgumentsTypes.None() && argumentsTypes.FirstOrDefault() is StructType pointerOwner) {

				var potentialField = BindFields(pointerOwner).FirstOrDefault(field => field.Name == call.Name);
				var potentialFieldCallArgsTypes = argumentsTypes.Skip(1).ToList();

				if (potentialField?.Type is FunctionPointerType pointer && pointer.Arity == potentialFieldCallArgsTypes.Count) {

					var argsAreCompatible = potentialFieldCallArgsTypes.All((argType, i) => IsAssignableTo(argType, pointer.Parameters[i]));

					if (argsAreCompatible) {
						return new FunctionCallInfo(new FunctionPointerField(pointerOwner, call.Name, pointer), ImmutableHashSet<Substitution>.Empty);
					}
				}
			}

			var overloads = GetOverloadsFor(call);

			return BindFunctionCallCore(overloads: overloads,
			                            argumentsTypes: argumentsTypes,
			                            explicitTypeArguments: explicitTypeArgumentsTypes,
			                            isPoly: call.IsPoly,
			                            location: new CallLocation(call.File, call.Line, getCallString()));

			string getCallString() {
				var explicitList = call.HasExplicitTypeArguments ? $"<{string.Join(",", call.ExplicitTypeArguments.Select(x => x.FullName))}>" : "";
				return $"{call.Name}{explicitList}({string.Join(",", argumentsTypes.Select(x => x.Name))})";
			}
		}


		private FunctionCallInfo BindFunctionCallCore(IReadOnlyList<Callable> overloads,
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