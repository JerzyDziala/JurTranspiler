using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;

namespace JurTranspiler.syntax_tree.bases {

    public abstract class Type : IType {

        public abstract ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public abstract ImmutableArray<ITreeNode> AllChildren { get; }

        public abstract IType WithSubstitutedTypes(ISet<Substitution> typeMap);
        public abstract string Name { get; }


        protected ImmutableArray<ITreeNode> GetAllChildren() {
            return this.ImmediateChildren.Concat(ImmediateChildren.SelectManyRecursive(x => x.ImmediateChildren)).ToImmutableArray();
        }
    }

}