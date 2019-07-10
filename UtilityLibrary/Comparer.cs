using System;
using System.Collections.Generic;

namespace UtilityLibrary {

	public class Comparer<T> : IEqualityComparer<T> {

		private readonly Func<T, T, bool> equalityComparer;
		private readonly Func<T, int> hashCodeGetter;

		public static Comparer<G> MakeComp<G>(Func<G, G, bool> eq, Func<G, int> hash) => new Comparer<G>(eq, hash);


		private Comparer(Func<T, T, bool> equalityComparer, Func<T, int> hashCodeGetter) {
			this.equalityComparer = equalityComparer;
			this.hashCodeGetter = hashCodeGetter;
		}


		public bool Equals(T first, T second) => equalityComparer(first, second);

		public int GetHashCode(T value) => hashCodeGetter(value);

	}

}