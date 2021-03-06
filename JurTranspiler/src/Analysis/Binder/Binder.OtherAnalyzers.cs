using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.expressions;
using JurTranspiler.syntax_tree.Interfaces;
using JurTranspiler.syntax_tree.statements;
using UtilityLibrary;

namespace JurTranspiler.Analysis.Binder {

	public partial class Binder {

		private void PerformOtherChecks() {
			CheckForInvalidAssignments();
			CheckForInvalidInitializers();
			CheckForInvalidUseOfNew();
		}


		private void CheckForInvalidAssignments() {
			
			var assignments = symbols.Tree.AllAssignments;

			foreach (var assignment in assignments) {

				if (assignment is AssignmentStatementSyntax syntax) {

					//syntactically incorrect assignments
					if (!syntax.Left.CanSyntacticallyBeAssignedTo) {
						errors.Add(new InvalidAssignment(assignment.Location));
						continue;
					}

					//assigning to immutable variables
					if (syntax.Left is VariableAccessSyntax variableAccess
					    && variableAccess.GetVisibleDefinitionOrNull() is { } existingDeclaration
					    && !existingDeclaration.IsMutable
					    ||
					    syntax.Left is FieldAccessSyntax fieldAccess
					    && GetVisibleDefinitionOrNull(fieldAccess) is {} existingDefinition
					    && !existingDefinition.IsMutable) {

						errors.Add(new AssigningToImmutableValue(syntax.Location));
					}
				}

				var (leftType, rightType) = BindAssignment(assignment);
				
				if (!IsAssignableTo(rightType, leftType)) {
					errors.Add(new TypeMismatchInAssignmentError(file: assignment.File,
					                                             line: assignment.Line,
					                                             leftName: leftType.Name,
					                                             rightName: rightType.Name));
				}
			}
		}

		private void CheckForInvalidInitializers() {

			var initializers = symbols.Tree.AllInitializers;

			foreach (var initializer in initializers) {

				var initializedType = BindType(initializer.ParentConstructor.ConstructedType) as StructType;

				if (initializedType is null)
					continue;

				var assignedField = BindFields(initializedType).FirstOrDefault(x => x.Name == initializer.FieldName);
				if (assignedField == null) {
					errors.Add(new NoMatchingFieldFound(initializer.Location,
					                                    initializer.FieldName,
					                                    initializedType.Name));
					return;
				}
				var initializerExpressionType = BindExpression(initializer.Expression);
				if (!IsAssignableTo(initializerExpressionType, assignedField.Type)) {
					errors.Add(new TypeMismatchInAssignmentError(initializer.Location,
					                                             assignedField.Type.Name,
					                                             initializerExpressionType.Name));
				}
			}
		}


		private void CheckForInvalidUseOfNew() {

			var constructors = symbols.Tree.AllConstructors;

			foreach (var constructor in constructors) {
				var type = BindType(constructor.ConstructedType);
				if (type is UndefinedType || type is UndeclaredStructType)
					continue;

				//TODO: add constraints check when adding more type parameters constraints
				if (type is TypeParameterType typeParameterType) {
//					typeParameterType.GetAllConstraints()[0].
				}

				if (type.IsNot<StructType>() && type.IsNot<ArrayType>()) {

					errors.Add(new InvalidUseOfNew(constructor.Location, type.Name));
				}
			}
		}

	}

}