using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.semantic_model {

	public abstract class Type : ITreeNode {

		protected abstract bool IsAssignableToCore(Type type, HashSet<Error> errors);

		//memoized version of IsAssignableToCore
		private Func<Type, HashSet<Error>, bool> isAssignableTo;
		public Func<Type, HashSet<Error>, bool> IsAssignableTo {
			get {
				if (isAssignableTo == null) {
					isAssignableTo = MemoizeIsAssignableTo(IsAssignableToCore);
					return isAssignableTo;
				}
				return isAssignableTo;
			}
		}


		private static Func<Type, HashSet<Error>, bool> MemoizeIsAssignableTo(Func<Type, HashSet<Error>, bool> func) {
			var cache = new Dictionary<Type, bool>();
			return (type, errors) => {
				       if (!cache.ContainsKey(type)) {
					       cache[type] = func(type, errors);
				       }
				       return cache[type];
			       };
		}


		public abstract Type WithSubstitutedTypes(ISet<Substitution> typeMap);

		public abstract bool IsAssignableToWithSubstitutions(Type type, ICollection<Substitution> substitutions, HashSet<Error> errors);

		public abstract bool IsEqualToWithSubstitutions(Type type, ICollection<Substitution> substitutions, HashSet<Error> errors);

		public abstract string Name { get; }

		public virtual string GetDefaultValue() => "null";

		public abstract ImmutableList<ITreeNode> ImmediateChildren { get; }

		public abstract ImmutableList<ITreeNode> AllChildren { get; }

		public abstract string GetJsTypeCacheGetter();

	}

}