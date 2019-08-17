using System.Collections.Immutable;
using System.Linq;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {

        private int incr;

        private readonly object o = new object();


        private int GetUniqueId() {
            lock (o) {
                incr++;
                return incr;
            }
        }


        public void GenerateNewCallableNames() {
            //TODO: check for mistakes
            symbols.FunctionSignaturesBindings.Values.ToImmutableArray()
                   .Where(x => !x.IsExtern)
                   .GroupBy(x => x.Name)
                   .ToList()
                   .ForEach(g => {
                       foreach (var f in g) symbols.NewNames.Add(f, f.Name + "$" + GetUniqueId().ToString());
                   });
        }

    }

}