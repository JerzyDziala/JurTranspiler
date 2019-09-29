using System;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.declarations;
using JurTranspiler.syntax_tree.Interfaces;
using JurTranspiler.syntax_tree.statements;

namespace JurTranspiler.Analysis.Binder {

	public partial class Binder {

		private (IType left, IType right) BindAssignment(IAssignment syntax) {
			return syntax switch{
				       AssignmentStatementSyntax assignmentSyntax => (BindExpression(assignmentSyntax.Left), BindExpression(assignmentSyntax.Right)),
				       InitializedVariableDeclarationSyntax variable => (BindType(variable.Type!), BindExpression(variable.Initializer)),
				       _ => throw new Exception("unknown type of assignment")
				       };
		}

	}

}