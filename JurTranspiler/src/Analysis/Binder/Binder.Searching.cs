using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.semantic_model;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.declarations;
using JurTranspiler.syntax_tree.expressions;
using JurTranspiler.syntax_tree.Interfaces;
using JurTranspiler.syntax_tree.types;
using UtilityLibrary;

namespace JurTranspiler.Analysis.Binder {
    
    public partial class Binder{
        
        //structs
        
        private StructDefinitionSyntax? GetVisibleDefinitionOrNull(StructTypeSyntax syntax) {
            var definitions = GetVisibleDefinitionsFor(syntax);
            return definitions.Any()
                       ? definitions.First()
                       : null;
        }


        private ImmutableArray<StructDefinitionSyntax> GetVisibleDefinitionsFor(StructTypeSyntax syntax) {
            return GetStructDefinitionsWith(syntax.Name, syntax.Arity)
                   .Where(def => def.Abstraction <= syntax.Abstraction)
                   .ToImmutableArray();
        }

        private ImmutableArray<StructDefinitionSyntax> GetStructDefinitionsWith(string name, int arity) {
            return symbols.Tree.AllStructDefinitions.Where(definition => definition.Name == name && definition.GenericArity == arity).ToImmutableArray();
        }
        
        private ImmutableArray<FunctionDefinitionSyntax> GetFunctionDefinitionsWith(string name, int argsCount) {
            return symbols.Tree.AllFunctionDefinitions.Where(definition => definition.Name == name && definition.Parameters.Length == argsCount).ToImmutableArray();
        }
        
        //functions

        private ImmutableArray<FunctionDefinitionSyntax> GetVisibleDefinitionsFor(FunctionCallSyntax syntax) {
            return GetFunctionDefinitionsWith(syntax.Name, syntax.Arguments.Length)
                   .Where(def => !def.IsPrivate || syntax.File == def.File)
                   .Where(def => def.Abstraction <= syntax.Abstraction)
                   .Where(def => def.TypeParameters.Length >= syntax.ExplicitTypeArguments.Length)
                   .ToImmutableArray();
        }
        
        //fields

        private ImmutableArray<Field> GetVisibleDefinitionsFor(FieldAccessSyntax syntax) {
            
            var ownerType = BindExpression(syntax.Owner);
            
			if (ownerType is StructType structType) {
				var fields = BindFields(structType);
				var matchingFields = fields.Where(field => field.Name == syntax.Name).ToImmutableArray();
				return matchingFields;
			}
            
            return ImmutableArray<Field>.Empty;
        }

        private FieldDeclarationSyntax? GetVisibleDefinitionOrNull(FieldAccessSyntax syntax) {
            var definitions = GetVisibleDefinitionsFor(syntax).Select(x=>x.OriginalSyntax).ToImmutableArray();
            return definitions.Length >= 1 ? definitions.First() : null;
        }
        

        //typeParameters

        private IStructOrFunctionDeclarationSyntax? GetParentDeclarationFor(TypeParameterSyntax syntax) {
            return syntax.AllParents.FirstOrDefault(p => p is IStructOrFunctionDeclarationSyntax) as IStructOrFunctionDeclarationSyntax;
        }
    }
}