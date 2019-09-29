using System.Collections.Immutable;

namespace JurTranspiler.syntax_tree.Interfaces {

    public interface ITreeNode {
        ImmutableArray<ITreeNode> ImmediateChildren { get; }
        ImmutableArray<ITreeNode> AllChildren { get; }
    }



}