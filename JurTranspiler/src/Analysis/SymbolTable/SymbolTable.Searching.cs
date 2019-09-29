using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.syntax_tree.declarations;
using JurTranspiler.syntax_tree.expressions;
using JurTranspiler.syntax_tree.types;

namespace JurTranspiler.Analysis.SymbolTable {

    public partial class SymbolTable {

        public SyntaxTree Tree { get; }


        public SymbolTable(SyntaxTree tree) {
            Tree = tree;

        }


        //searching


        public StructDefinitionSyntax? GetVisibleDefinitionOrNull(StructTypeSyntax syntax) {
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
                   .Where(def => !def.IsPrivate || syntax.File == def.File)
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


        public IStructOrFunctionDeclarationSyntax? GetParentDeclarationFor(TypeParameterSyntax syntax) {
            return syntax.AllParents.FirstOrDefault(p => p is IStructOrFunctionDeclarationSyntax) as IStructOrFunctionDeclarationSyntax;
        }

    }

}