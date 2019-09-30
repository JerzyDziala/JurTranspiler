using System.Collections.Immutable;
using System.Linq;
using UtilityLibrary;

namespace JurTranspiler.Analysis.Binder {

    public partial class Binder {

        private int incr;

        private readonly object o = new object();


        private int GetUniqueId() {
            lock (o) {
                incr++;
                return incr;
            }
        }


        private void GenerateNewCallableNames() {
            //TODO: check for mistakes
            
            
            symbols.FunctionSignaturesBindings.Values.ToImmutableArray()
                   .Where(x => !x.IsExtern)
                   .GroupBy(x => x.Name)
                   .ForEach(g => {
                       foreach (var f in g) symbols.NewFunctionNames.Add(f, f.Name + "$" + GetUniqueId().ToString());
                   });
        }


        private void GenerateNewVariableNames()
        {
            symbols.Tree.VariableDeclarations
                   .ForEach(declaration => symbols.NewVariableNames[declaration] = declaration.Name + "$" + GetUniqueId().ToString());
        }

    }

}