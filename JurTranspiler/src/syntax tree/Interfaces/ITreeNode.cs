using System.Collections.Immutable;

namespace JurTranspiler.compilerSource.nodes {

    public interface ITreeNode {
        ImmutableList<ITreeNode> ImmediateChildren { get; }
        ImmutableList<ITreeNode> AllChildren { get; }
    }

}