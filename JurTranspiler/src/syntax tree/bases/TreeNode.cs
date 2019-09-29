using System.Collections.Immutable;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.syntax_tree.bases {

    abstract class TreeNode : ITreeNode {
        public abstract ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public abstract ImmutableArray<ITreeNode> AllChildren { get; }
    }

}