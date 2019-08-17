using System.Collections.Generic;
using System.Linq;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.Analysis {

    public static class ErrorExtensions {
        public static IEnumerable<(string file, int line)> GetLocations(this IEnumerable<ISyntaxNode> syntaxs) {
            return syntaxs.Select(x => (x.File, x.Line));
        }
    }

}