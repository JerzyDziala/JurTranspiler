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


        private int GetUniqueId() {
            lock (o) {
                incr++;
                return incr;
            }
        }


        public void GenerateNewCallableNames() {
            //TODO: check for mistakes
            int id;
            symbols.FunctionSignaturesBindings.Values.ToImmutableList()
                   .Where(x => !x.IsExtern)
                   .GroupBy(x => x.Name)
                   .ToList()
                   .ForEach(g => {
                       id = 0;
                       foreach (var f in g) symbols.NewNames.Add(f, f.Name + "$" + GetUniqueId());
                   });
        }

    }

}