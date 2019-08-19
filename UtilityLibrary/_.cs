using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UtilityLibrary {

	public static class _ {

		public static R? As<R>(this object obj) where R : class {
			return obj as R;
		}


		public static bool TrueForAllPairs<A, B>(IReadOnlyList<A> listA, IReadOnlyList<B> listB, Func<A, B, bool> predicate) {
			Debug.Assert(listA.Count == listB.Count);
			return listA.All((a, i) => predicate(a, listB[i]));
		}

	}

}