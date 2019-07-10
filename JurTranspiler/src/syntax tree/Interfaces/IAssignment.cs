using System.Collections.Generic;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.nodes {

    public interface IAssignment : IStatementSyntax{
        Type GetLeftType(HashSet<Error> errors, Binder binder);
        Type GetRightType(HashSet<Error> errors, Binder binder);
    }

}