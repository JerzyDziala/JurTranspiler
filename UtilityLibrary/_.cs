using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UtilityLibrary {

    public static class _ {

        //-------------------------------------------------------utils-----------------------------------------------------//


        public static R As<R>(this object obj) where R : class {
            return obj as R;
        }


        public static bool TrueForAllPairs<A, B>(IReadOnlyList<A> listA, IReadOnlyList<B> listB, Func<A, B, bool> predicate) {
            Debug.Assert(listA.Count == listB.Count);
            return listA.All((a, i) => predicate(a, listB[i]));
        }


        public static IEnumerable<C> Select<A, B, C>(IReadOnlyList<A> listA, IReadOnlyList<B> listB, Func<A, B, C> combinator) {
            Debug.Assert(listA.Count == listB.Count);
            return listA.WithIndexes().Select(x => combinator(x.value, listB[x.i]));
        }

    }

}