using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace UtilityLibrary {

	public static class CollectionsExtensions {

		public static ImmutableArray<T> AddIfNotNull<T>(this ImmutableArray<T> list, T? item) where T : class {
			return item == null ? list : list.Add(item!);
		}


		public static bool None<T>(this IEnumerable<T> list, Func<T, bool> predicate) {
			return !list.Any(predicate);
		}


		public static bool None<T>(this IEnumerable<T> list) {
			return !list.Any();
		}


		public static void ForEach<T>(this IEnumerable<T> list, Action<T> action) {
			list.ToList().ForEach(action);
		}


		public static void ForEach<T>(this IEnumerable<T> list, Action<T, int> action) {
			list.Select((x, i) => (x, i)).ForEach(x => action(x.x, x.i));
		}


		public static bool One<T>(this IEnumerable<T> list) {
			return list.Count() == 1;
		}


		public static bool One<T>(this IEnumerable<T> list, Func<T, bool> predicate) {
			return list.Where(predicate).One();
		}


		public static bool AllHaveSame<T, G>(this IEnumerable<T> list, Func<T, G> selector) {
			var enumerable = list.ToList();
			var first = selector(enumerable.FirstOrDefault());
			return first == null || enumerable.All(element => first.Equals(selector(element)));
		}


		public static bool AllAreSame<T>(this IEnumerable<T> list) {
			var enumerable = list.ToList();
			var first = enumerable.FirstOrDefault();

			return first == null || enumerable.All(element => first.Equals(element));
		}


		public static List<IGrouping<K, V>> GetDuplicates<K, V>(this IEnumerable<V> list, Func<V, K> selector) {
			return list.GroupBy(selector).Where(g => g.MoreThenOne()).ToList();
		}


		public static void AddRange<T>(this ICollection<T> set, IEnumerable<T> collection) {
			foreach (var element in collection) {
				set.Add(element);
			}
		}


		public static ImmutableArray<T> AsImmutableArray<T>(this T obj) {
			return ImmutableArray.Create(obj);
		}


		public static bool IsSuperSetOf<T>(this IEnumerable<T> a, IEnumerable<T> b) {
			return !ReferenceEquals(a, b) && a.All(b.Contains);
		}


		public static IEnumerable<(T value, int i)> WithIndexes<T>(this IEnumerable<T> sequence) => sequence.Select((t, i) => (t, i));


		public static bool All<T>(this IEnumerable<T> enumerable, Func<T, int, bool> predicate) {
			return enumerable.WithIndexes().All(item => predicate(item.value, item.i));
		}


		public static bool MoreThenOne<T>(this IEnumerable<T> collection) {
			return collection.Count() > 1;
		}


		public static string Glue(this IEnumerable<string> enumerable, string s = "") {
			return string.Join(s, enumerable);
		}


		public static IEnumerable<T> SelectManyRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector) {
			var result = source.SelectMany(selector).ToList();
			return result.None() ? result : result.Concat(result.SelectManyRecursive(selector));
		}

	}

}