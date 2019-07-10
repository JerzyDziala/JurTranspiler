using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model.functions;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {

        private int incr = 0;

        private object o = new object();


        public int GetUniqueId() {
            lock (o) {
                incr++;
                return incr;
            }
        }


        private Dictionary<ICallable, string> NewNames = new Dictionary<ICallable, string>();


        public void GenerateNewCallableNames() {
            int id;
            symbols.FunctionSignaturesBindings.Values.ToImmutableList()
                   .Where(x => !x.IsExtern)
                   .GroupBy(x => x.Name)
                   .ToList()
                   .ForEach(g => {
                       id = 0;
                       foreach (var f in g) NewNames.Add(f, f.Name + "$" + id++);
                   });
        }


        public string GetNewNameFor(FunctionDefinitionSyntax definition) {
            return definition.IsExtern
                       ? definition.Name
                       : NewNames[symbols.FunctionSignaturesBindings[definition]];
        }


        public string GetNewNameFor(FunctionCallSyntax call, HashSet<Error> errors = null) {
            var callable = BindFunctionCall(call, errors ?? new HashSet<Error>());

            if (callable is Dispatcher d) return d.Name + $"$Arity_{d.Arity}_dispatcher";

            if (callable is FunctionPointer pointer) return pointer.Name;

            if (callable is FunctionSignature signature) {
                return signature.IsExtern ? signature.Name : NewNames[signature];
            }

            throw new Exception("impossible");
        }


        /*
         * stringify<poly>(someExpression());
         * -----
         * stringify_Arity_1_dispatcher$([someExpression()])
         *
         * ...
         * stringify_Arity_1_dispatcher$(argsList) {
         *    const argsTypes = argsList.map(x->getType$(x));
         * 	  return stringify
         * }
         *
         * getType$(value){
         * 	  if(typeof value === 'string') return new PrimitiveType('string');
         * 	  if(typeof value === 'number') return new PrimitiveType('num');
         * 	  if(typeof value === 'boolean') return new PrimitiveType('bool');
         * 	  if(typeof value === 'object') return value.type$;
         * }
         * ...
         *
         * stringify$1(s) {}
         *
         * stringify$2(a) {}
         *
         * stringify$3(l) {}
         */

    }

}