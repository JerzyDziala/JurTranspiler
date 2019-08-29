using System.Collections.Immutable;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.syntax_tree.Interfaces {

    public interface IControlFlowStatement {
        ImmutableArray<ImmutableArray<IControlFlowStatement>> Cases { get; }
    }





}