using System.Collections.Immutable;

namespace JurTranspiler.compilerSource.nodes.bases {

    abstract class TreeNode : ITreeNode {
        public abstract ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public abstract ImmutableArray<ITreeNode> AllChildren { get; }
    }

}