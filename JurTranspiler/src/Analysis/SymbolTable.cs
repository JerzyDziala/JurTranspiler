using System.Collections.Generic;
using System.Collections.Immutable;
using System.Dynamic;
using System.Linq;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.compilerSource.semantic_model.functions;
using JurTranspiler.src.syntax_tree.types;

namespace JurTranspiler.compilerSource.Analysis
{

    public class SymbolTable
    {

        public ImmutableList<StructDefinitionSyntax> structDefinitions { get; }
        public ImmutableList<ITypeSyntax> typeUsages { get; }
        public ImmutableList<IExpressionSyntax> expressions { get; }
        public ImmutableList<FunctionDefinitionSyntax> functionDefinitions { get; }

        public Dictionary<StructDefinitionSyntax, Type> OpenStructsBinding { get; } = new Dictionary<StructDefinitionSyntax, Type>();
        public Dictionary<ITypeSyntax, Type> TypesBindings { get; } = new Dictionary<ITypeSyntax, Type>();
        public Dictionary<FunctionDefinitionSyntax, FunctionSignature> FunctionSignaturesBindings { get; } = new Dictionary<FunctionDefinitionSyntax, FunctionSignature>();
        public Dictionary<FunctionCallSyntax, ICallable> FunctionCallsBindings { get; } = new Dictionary<FunctionCallSyntax, ICallable>();


        public SymbolTable(SyntaxTree tree)
        {
            structDefinitions = tree.AllChildren.OfType<StructDefinitionSyntax>().ToImmutableList();
            typeUsages = tree.AllChildren.OfType<ITypeSyntax>().ToImmutableList();
            expressions = tree.AllChildren.OfType<IExpressionSyntax>().ToImmutableList();
            functionDefinitions = tree.AllChildren.OfType<FunctionDefinitionSyntax>().ToImmutableList();
        }


        //searching
        public ImmutableList<IVariableDeclarationSyntax> GetVisibleVariablesInScope(ISyntaxNode scope)
        {

            var declarations = new List<IVariableDeclarationSyntax>();
            ITreeNode previousScope = scope;
            var parentScope = scope.Parent;

            while (parentScope != null)
            {
                foreach (var node in parentScope.ImmediateChildren)
                {
                    if (node == previousScope)
                        break;
                    if (node is IVariableDeclarationSyntax declaration)
                    {
                        declarations.Add(declaration);
                    }
                }
                previousScope = parentScope;
                parentScope = parentScope.Parent;
            }

            return declarations.ToImmutableList();
        }


        public StructDefinitionSyntax GetVisibleDefinitionOrNull(StructTypeSyntax syntax)
        {
            var definitions = GetVisibleDefinitionsFor(syntax);
            return definitions.Any()
                       ? definitions.First()
                       : null;
        }


        public ImmutableList<StructDefinitionSyntax> GetVisibleDefinitionsFor(StructTypeSyntax syntax)
        {
            return GetStructDefinitionsWith(syntax.Name, syntax.Arity)
                   .Where(def => def.Abstraction <= syntax.Abstraction)
                   .ToImmutableList();
        }


        public ImmutableList<FunctionDefinitionSyntax> GetVisibleDefinitionsFor(FunctionCallSyntax syntax)
        {
            return GetFunctionDefinitionsWith(syntax.Name, syntax.Arguments.Count)
                   .Where(def => def.Abstraction <= syntax.Abstraction)
                   .Where(def => def.TypeParametersInGenericTypesList.Count >= syntax.ExplicitTypeArguments.Count)
                   .ToImmutableList();
        }


        public ImmutableList<StructDefinitionSyntax> GetStructDefinitionsWith(string name, int arity)
        {
            return structDefinitions.Where(definition => definition.Name == name && definition.Arity == arity).ToImmutableList();
        }


        public ImmutableList<FunctionDefinitionSyntax> GetFunctionDefinitionsWith(string name, int argsCount)
        {
            return functionDefinitions.Where(definition => definition.Name == name && definition.Parameters.Count == argsCount).ToImmutableList();
        }


        public IStructOrFunctionDeclarationSyntax GetParentDeclarationFor(TypeParameterSyntax syntax)
        {
            return syntax.AllParents.FirstOrDefault(p => p is IStructOrFunctionDeclarationSyntax) as IStructOrFunctionDeclarationSyntax;
        }


        //Bindings-Caching
        public bool AlreadyBound(StructDefinitionSyntax syntax) => OpenStructsBinding.ContainsKey(syntax);
        public bool AlreadyBound(ITypeSyntax syntax) => TypesBindings.ContainsKey(syntax);
        public bool AlreadyBound(FunctionDefinitionSyntax syntax) => FunctionSignaturesBindings.ContainsKey(syntax);
        public bool AlreadyBound(FunctionCallSyntax syntax) => FunctionCallsBindings.ContainsKey(syntax);

        public Type GetBindingFor(StructDefinitionSyntax syntax) => OpenStructsBinding[syntax];
        public Type GetBindingFor(ITypeSyntax syntax) => TypesBindings[syntax];
        public FunctionSignature GetBindingFor(FunctionDefinitionSyntax syntax) => FunctionSignaturesBindings[syntax];
        public ICallable GetBindingFor(FunctionCallSyntax syntax) => FunctionCallsBindings[syntax];


        public Type MakeBindingFor(StructDefinitionSyntax syntax, Type value)
        {
            OpenStructsBinding[syntax] = value;
            return value;
        }


        public Type MakeBindingFor(ITypeSyntax syntax, Type value)
        {
            if (!(value is UndeclaredStructType))
            {
                TypesBindings[syntax] = value;
            }
            return value;
        }


        public FunctionSignature MakeBindingFor(FunctionDefinitionSyntax syntax, FunctionSignature value)
        {
            FunctionSignaturesBindings[syntax] = value;
            return value;
        }


        public ICallable MakeBindingFor(FunctionCallSyntax syntax, ICallable value)
        {
            FunctionCallsBindings[syntax] = value;
            return value;
        }

    }

}