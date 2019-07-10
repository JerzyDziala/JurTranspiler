using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.src.syntax_tree.types;

namespace JurTranspiler.compilerSource.syntax_tree.searching {

    public static class TreeSearchingExtensions {

        private static ImmutableList<IVariableDeclarationSyntax> GetVisibleDeclarations(this VariableAccessSyntax accessSyntax) {
            return accessSyntax.GetVisibleVariablesInScope().Where(x=>x.Name == accessSyntax.Name).ToImmutableList();
        }

		public static ImmutableList<IVariableDeclarationSyntax> GetVisibleVariablesInScope(this ISyntaxNode scope) {

			var declarations = new List<IVariableDeclarationSyntax>();
			ITreeNode previousScope = scope;
			var parentScope = scope.Parent;

			while (parentScope != null) {
				foreach (var node in parentScope.ImmediateChildren) {
					if (node == previousScope) break;
					if (node is IVariableDeclarationSyntax declaration) {
						declarations.Add(declaration);
					}
				}
				previousScope = parentScope;
				parentScope = parentScope.Parent;
			}

			return declarations.ToImmutableList();
		}

        public static IVariableDeclarationSyntax GetVisibleDeclarationOrNull(this VariableAccessSyntax variableAccess) {
            var declarations = variableAccess.GetVisibleDeclarations();
            return declarations.Any()
                       ? declarations.First()
                       : null;

        }


    }

}