using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.Analysis {

    public class Error : IEquatable<Error> {

        public virtual string GetMessage() => "";


        public virtual bool Equals(Error other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(GetMessage(), other.GetMessage());
        }


        public override string ToString() => GetMessage() + "\n";

        public override int GetHashCode() => GetMessage().GetHashCode();
    }


    public static class ErrorExtensions {
        public static IEnumerable<(string file, int line)> GetLocations(this IEnumerable<ISyntaxNode> syntaxs) {
            return syntaxs.Select(x => (x.File, x.Line));
        }
    }

}