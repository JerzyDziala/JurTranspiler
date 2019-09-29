using System.Collections.Generic;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.semantic_model.types {

    public interface IType : ITreeNode {
        IType WithSubstitutedTypes(ISet<Substitution> typeMap);
        string Name { get; }
    }

}