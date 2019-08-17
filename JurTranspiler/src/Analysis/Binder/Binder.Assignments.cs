using System;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {

        public (IType left, IType right) BindAssignment(IAssignment syntax) {
            return syntax switch{
                       AssignmentStatementSyntax assignmentSyntax => (BindExpression(assignmentSyntax.Left), BindExpression(assignmentSyntax.Right)),
                       InitializedVariableDeclarationSyntax variable => (BindType(variable.Type), BindExpression(variable.Initializer)),
                       _ => throw new Exception("unknown type of assignment")
                       };
        }

    }

}