using System.Collections.Generic;
using System.Collections.Immutable;
using System.Dynamic;
using System.Linq;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.compilerSource.semantic_model.functions;
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


        private ImmutableList<IVariableDeclarationSyntax> GetVisibleDeclarations(VariableAccessSyntax accessSyntax) {
            return GetVisibleVariablesInScope(accessSyntax).Where(x => x.Name == accessSyntax.Name).ToImmutableList();
        }


        public ImmutableList<IVariableDeclarationSyntax> GetVisibleVariablesInScope(ISyntaxNode scope) {

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


        public StructDefinitionSyntax GetVisibleDefinitionOrNull(StructTypeSyntax syntax) {
            var definitions = GetVisibleDefinitionsFor(syntax);
            return definitions.Any()
                       ? definitions.First()
                       : null;
        }


        public ImmutableList<StructDefinitionSyntax> GetVisibleDefinitionsFor(StructTypeSyntax syntax) {
            return GetStructDefinitionsWith(syntax.Name, syntax.Arity)
                   .Where(def => def.Abstraction <= syntax.Abstraction)
                   .ToImmutableList();
        }


        public ImmutableList<FunctionDefinitionSyntax> GetVisibleDefinitionsFor(FunctionCallSyntax syntax) {
            return GetFunctionDefinitionsWith(syntax.Name, syntax.Arguments.Count)
                   .Where(def => def.Abstraction <= syntax.Abstraction)
                   .Where(def => def.TypeParametersInGenericTypesList.Count >= syntax.ExplicitTypeArguments.Count)
                   .ToImmutableList();
        }


        public ImmutableList<StructDefinitionSyntax> GetStructDefinitionsWith(string name, int arity) {
            return Tree.AllStructDefinitions.Where(definition => definition.Name == name && definition.Arity == arity).ToImmutableList();
        }


        public ImmutableList<FunctionDefinitionSyntax> GetFunctionDefinitionsWith(string name, int argsCount) {
            return Tree.AllFunctionDefinitions.Where(definition => definition.Name == name && definition.Parameters.Count == argsCount).ToImmutableList();
        }


        public IStructOrFunctionDeclarationSyntax GetParentDeclarationFor(TypeParameterSyntax syntax) {
            return syntax.AllParents.FirstOrDefault(p => p is IStructOrFunctionDeclarationSyntax) as IStructOrFunctionDeclarationSyntax;
        }

    }

}