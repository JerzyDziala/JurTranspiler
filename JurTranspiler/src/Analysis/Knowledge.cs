using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.compilerSource.semantic_model.functions;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.Analysis {

    public class Knowledge {

        public ImmutableArray<Type> AllTypes { get; }
        public ImmutableDictionary<ITypeSyntax, Type> TypesBindings { get; }
        public ImmutableDictionary<StructDefinitionSyntax, Type> StructDefinitionsBindings { get; }
        public ImmutableDictionary<StructType, ImmutableArray<Field>> Fields { get; }
        public ImmutableDictionary<FunctionDefinitionSyntax, FunctionSignature> FunctionSignaturesBindings { get; }
        public ImmutableDictionary<FunctionCallSyntax, FunctionCallInfo> FunctionCallsBindings { get; }
        private ImmutableDictionary<ICallable, string> NewNames;


        public Knowledge(IEnumerable<Type> allTypes,
                         IDictionary<ITypeSyntax, Type> typesBindings,
                         IDictionary<StructType, ImmutableArray<Field>> fields,
                         IDictionary<ICallable, string> newNames,
                         IDictionary<FunctionDefinitionSyntax, FunctionSignature> functionSignaturesBindings,
                         IDictionary<FunctionCallSyntax, FunctionCallInfo> functionCallsBindings,
                         IDictionary<StructDefinitionSyntax, Type> structDefinitionsBindings) {

            StructDefinitionsBindings = structDefinitionsBindings.ToImmutableDictionary();
            FunctionCallsBindings = functionCallsBindings.ToImmutableDictionary();
            FunctionSignaturesBindings = functionSignaturesBindings.ToImmutableDictionary();
            NewNames = newNames.ToImmutableDictionary();
            Fields = fields.ToImmutableDictionary();
            AllTypes = allTypes.ToImmutableArray();
            TypesBindings = typesBindings.ToImmutableDictionary();
        }


        public string GetNewNameFor(FunctionDefinitionSyntax definition) {
            return definition.IsExtern
                       ? definition.Name
                       : NewNames[FunctionSignaturesBindings[definition]];
        }


        public string GetNewNameFor(FunctionCallSyntax call) {
            var callable = FunctionCallsBindings[call].Callable;

            if (callable is Dispatcher d) return d.Name + $"$Arity_{d.Arity}_dispatcher";

            if (callable is FunctionPointer pointer) return pointer.Name;

            if (callable is FunctionSignature signature) {
                return signature.IsExtern ? signature.Name : NewNames[signature];
            }

            throw new Exception("impossible");
        }
    }

}