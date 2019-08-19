using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;

namespace JurTranspiler.syntax_tree.bases {

	public abstract class Type : IType {

		public abstract ImmutableArray<ITreeNode> ImmediateChildren { get; }

		private ImmutableArray<ITreeNode>? allChildren;
		public ImmutableArray<ITreeNode> AllChildren {
			get {
				if (allChildren == null) {
					allChildren = GetAllChildren();
				}
				return allChildren.Value;
			}
		}

		public abstract IType WithSubstitutedTypes(ISet<Substitution> typeMap);
		public abstract string Name { get; }


		protected ImmutableArray<ITreeNode> GetAllChildren() {
			return ImmediateChildren.Concat(ImmediateChildren.SelectManyRecursive(x => x.ImmediateChildren)).ToImmutableArray();
		}

	}

}