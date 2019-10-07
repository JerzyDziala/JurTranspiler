using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.semantic_model;
using JurTranspiler.semantic_model.functions;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.declarations;
using JurTranspiler.syntax_tree.expressions;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.Analysis {

	public class Knowledge {

		public ImmutableArray<IType> AllTypes { get; }
		public ImmutableDictionary<ITypeSyntax, IType> TypesBindings { get; }
		public ImmutableDictionary<StructDefinitionSyntax, IType> StructDefinitionsBindings { get; }
		public ImmutableDictionary<StructType, ImmutableArray<Field>> Fields { get; }
		public ImmutableDictionary<FunctionDefinitionSyntax, FunctionSignature> FunctionSignaturesBindings { get; }
		public ImmutableDictionary<FunctionCallSyntax, FunctionCallInfo> FunctionCallsBindings { get; }
		public ImmutableDictionary<IExpressionSyntax, IType> ExpressionsBindings { get; }

		private readonly ImmutableDictionary<ICallable, string> NewCallableNames;
		private readonly ImmutableDictionary<IVariableDeclarationSyntax, string> NewVariableNames;


		public Knowledge(IEnumerable<IType> allTypes,
		                 IDictionary<ITypeSyntax, IType> typesBindings,
		                 IDictionary<StructType, ImmutableArray<Field>> fields,
		                 IDictionary<ICallable, string> newNames,
		                 IDictionary<IVariableDeclarationSyntax, string> newVariableNames,
		                 IDictionary<FunctionDefinitionSyntax, FunctionSignature> functionSignaturesBindings,
		                 IDictionary<FunctionCallSyntax, FunctionCallInfo> functionCallsBindings,
		                 IDictionary<StructDefinitionSyntax, IType> structDefinitionsBindings,
		                 IDictionary<IExpressionSyntax, IType> expressionsBindings) {
			NewVariableNames = newVariableNames.ToImmutableDictionary();
			NewCallableNames = newNames.ToImmutableDictionary();
			ExpressionsBindings = expressionsBindings.ToImmutableDictionary();
			StructDefinitionsBindings = structDefinitionsBindings.ToImmutableDictionary();
			FunctionCallsBindings = functionCallsBindings.ToImmutableDictionary();
			FunctionSignaturesBindings = functionSignaturesBindings.ToImmutableDictionary();
			Fields = fields.ToImmutableDictionary();
			AllTypes = allTypes.ToImmutableArray();
			TypesBindings = typesBindings.ToImmutableDictionary();
		}


		public string GetNewNameFor(FunctionDefinitionSyntax definition) {
			return definition.IsExtern
				       ? definition.Name
				       : NewCallableNames[FunctionSignaturesBindings[definition]];
		}


		public string GetNewNameFor(FunctionCallSyntax call) {
			var callable = FunctionCallsBindings[call].Callable;

			return callable switch {
				       Dispatcher d => throw new NotImplementedException("Dispatchers not implemented yet"),
				       FunctionPointer pointer => GetNewNameFor(pointer.declaration!),
				       FunctionSignature signature => signature.IsExtern ? signature.Name : NewCallableNames[signature],
				       _ =>throw new Exception("impossible")
				       };

		}


		public string GetNewNameFor(VariableAccessSyntax access) {
			return GetNewNameFor(access.GetVisibleDefinitionOrNull()!);
		}


		public string GetNewNameFor(IVariableDeclarationSyntax declaration) {
			return NewVariableNames[declaration];
		}

	}

}