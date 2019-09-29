using System.Collections.Immutable;

namespace JurTranspiler.syntax_tree.Interfaces {

    public interface IControlFlowStatement {
        ImmutableArray<ImmutableArray<IControlFlowStatement>> Cases { get; }
    }





}