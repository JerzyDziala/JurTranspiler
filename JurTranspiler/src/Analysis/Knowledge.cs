using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.compilerSource.semantic_model.functions;

namespace JurTranspiler.compilerSource.Analysis {

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

            if (callable is Dispatcher d) return d.Name + $"$Arity_{d.Arity.ToString()}_dispatcher";

            if (callable is FunctionPointer pointer) return pointer.Name;

            if (callable is FunctionSignature signature) {
                return signature.IsExtern ? signature.Name : NewCallableNames[signature];
            }

            throw new Exception("impossible");
        }


        public string GetNewNameFor(VariableAccessSyntax access) {
            return GetNewNameFor(access.GetVisibleDeclarationOrNull()!);
        }


        public string GetNewNameFor(IVariableDeclarationSyntax declaration) {
            return NewVariableNames[declaration];
        }
    }

}