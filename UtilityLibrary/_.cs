using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace UtilityLibrary {

	public static class _ {

        public static bool IsNot<T>(this object o) => !(o is T);

		public static R? As<R>(this object obj) where R : class {
			return obj as R;
		}


		public static bool TrueForAllPairs<A, B>(IReadOnlyList<A> listA, IReadOnlyList<B> listB, Func<A, B, bool> predicate) {
			Debug.Assert(listA.Count == listB.Count);
			return listA.All((a, i) => predicate(a, listB[i]));
		}

	}

}