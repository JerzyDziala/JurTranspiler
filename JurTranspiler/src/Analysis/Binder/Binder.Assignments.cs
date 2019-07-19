using System;
using System.Diagnostics;
using JurTranspiler.compilerSource.nodes;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {

        public (Type left, Type right) BindAssignment(IAssignment syntax) {
            return syntax switch{
                       AssignmentStatementSyntax assignmentSyntax => (BindExpression(assignmentSyntax.Left), BindExpression(assignmentSyntax.Right)),
                       InitializedVariableDeclarationSyntax variable => (BindType(variable.Type), BindExpression(variable.Initializer)),
                       _ => throw new Exception("unknown type of assignment")
                       };
        }

    }

}