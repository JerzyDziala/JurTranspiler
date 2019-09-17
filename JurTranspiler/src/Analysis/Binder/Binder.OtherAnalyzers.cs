using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.Analysis {

	public partial class Binder {

		private void PerformOtherChecks() {
			CheckForInvalidAssignments();
			CheckForInvalidInitializers();
			CheckForInvalidUseOfNew();
		}


		private void CheckForInvalidAssignments() {
			var assignments = symbols.Tree.AllAssignments;
			foreach (var assignment in assignments) {
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