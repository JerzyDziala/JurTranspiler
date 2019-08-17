using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.src.syntax_tree.types;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable {

        public SyntaxTree Tree { get; }


        public SymbolTable(SyntaxTree tree) {
            Tree = tree;

        }


        //searching
        public IVariableDeclarationSyntax GetVisibleDeclarationOrNull(VariableAccessSyntax variableAccess) {
            var declarations = GetVisibleDeclarations(variableAccess);
            return declarations.Any()
                       ? declarations.First()
                       : null;

        }


        private ImmutableArray<IVariableDeclarationSyntax> GetVisibleDeclarations(VariableAccessSyntax accessSyntax) {
            return GetVisibleVariablesInScope(accessSyntax).Where(x => x.Name == accessSyntax.Name).ToImmutableArray();
        }


        public ImmutableArray<IVariableDeclarationSyntax> GetVisibleVariablesInScope(ISyntaxNode scope) {

            var declarations = new List<IVariableDeclarationSyntax>();
            ITreeNode previousScope = scope;
            var parentScope = scope.Parent;

            while (parentScope != null) {
                foreach (var node in parentScope.ImmediateChildren) {
                    if (ReferenceEquals(node, previousScope)) break;
                    if (node is IVariableDeclarationSyntax declaration) {
                        declarations.Add(declaration);
                    }
                }
                previousScope = parentScope;
                parentScope = parentScope.Parent;
            }

            return declarations.ToImmutableArray();
        }


        public StructDefinitionSyntax GetVisibleDefinitionOrNull(StructTypeSyntax syntax) {
            var definitions = GetVisibleDefinitionsFor(syntax);
            return definitions.Any()
                       ? definitions.First()
                       : null;
        }


        public ImmutableArray<StructDefinitionSyntax> GetVisibleDefinitionsFor(StructTypeSyntax syntax) {
            return GetStructDefinitionsWith(syntax.Name, syntax.Arity)
                   .Where(def => def.Abstraction <= syntax.Abstraction)
                   .ToImmutableArray();
        }


        public ImmutableArray<FunctionDefinitionSyntax> GetVisibleDefinitionsFor(FunctionCallSyntax syntax) {
            return GetFunctionDefinitionsWith(syntax.Name, syntax.Arguments.Length)
                   .Where(def => def.Abstraction <= syntax.Abstraction)
                   .Where(def => def.TypeParameters.Length >= syntax.ExplicitTypeArguments.Length)
                   .ToImmutableArray();
        }


        public ImmutableArray<StructDefinitionSyntax> GetStructDefinitionsWith(string name, int arity) {
            return Tree.AllStructDefinitions.Where(definition => definition.Name == name && definition.GenericArity == arity).ToImmutableArray();
        }


        public ImmutableArray<FunctionDefinitionSyntax> GetFunctionDefinitionsWith(string name, int argsCount) {
            return Tree.AllFunctionDefinitions.Where(definition => definition.Name == name && definition.Parameters.Length == argsCount).ToImmutableArray();
        }


        public IStructOrFunctionDeclarationSyntax GetParentDeclarationFor(TypeParameterSyntax syntax) {
            return syntax.AllParents.FirstOrDefault(p => p is IStructOrFunctionDeclarationSyntax) as IStructOrFunctionDeclarationSyntax;
        }

    }

}