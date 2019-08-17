using System.Collections.Immutable;

namespace JurTranspiler.compilerSource.nodes {

    public interface ITreeNode {
        ImmutableArray<ITreeNode> ImmediateChildren { get; }
        ImmutableArray<ITreeNode> AllChildren { get; }
    }



}