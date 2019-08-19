using System.Collections.Generic;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.semantic_model {

    public interface IType : ITreeNode {
        IType WithSubstitutedTypes(ISet<Substitution> typeMap);
        string Name { get; }
    }

}