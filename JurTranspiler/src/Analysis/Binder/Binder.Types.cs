using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.Analysis.errors.bases;
using JurTranspiler.semantic_model;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.declarations;
using JurTranspiler.syntax_tree.Interfaces;
using JurTranspiler.syntax_tree.types;
using UtilityLibrary;

namespace JurTranspiler.Analysis.Binder {

    public partial class Binder {

        public IType BindStructDefinition(StructDefinitionSyntax syntax) {
            return symbols.AlreadyBound(syntax)
                       ? symbols.GetBindingFor(syntax)
                       : symbols.MakeBindingFor(syntax, BindStructDefinitionCore(syntax));
        }


        private IType BindStructDefinitionCore(StructDefinitionSyntax syntax) {

            //check for inheriting from non-struct types
            var invalidInlines = syntax.InlinedTypes.Where(x => !(x is StructTypeSyntax));
            var validInlines = syntax.InlinedTypes.OfType<StructTypeSyntax>();
            foreach (var inline in invalidInlines) {
                errors.Add(new InliningOfNonStructType(inline.File, inline.Line, inline.FullName));
            }

            //check for inheritance cycles
            var hasCycles = !IsAcyclic(syntax, new HashSet<StructDefinitionSyntax>());
            if (hasCycles) {
                errors.Add(new StructDeclarationContainsInheritanceCycles(syntax.File, syntax.Line, syntax.FullName));
            }

            //check for duplicate typeParameters
            var duplicates = syntax.TypeParameters
                                   .GroupBy(x => x.FullName)
                                   .Where(x => x.MoreThenOne())
                                   .ToList();
            foreach (var group in duplicates) {
                errors.Add(new MultipleTypeParametersWithTheSameName(group.Select(x => new Location(x)), group.First().FullName));
            }
            if (duplicates.Any()) return new UndeclaredStructType(syntax.FullName);

            //we want the construction of typeArguments to happen lazily only after the struct constructor has run
            //but to create a FullName for the StructType we need to eagerly load their names
            var typeArgumentsNames = syntax.TypeParameters.Select(p => p.FullName);
            var lazyTypeArguments = syntax.TypeParameters.Select(p => new Lazy<IType>(() => BindType(p)));

            var lazyInlinedTypes = hasCycles ? new Lazy<IType>[0] : validInlines.Select(inline => new Lazy<IType>(() => BindType(inline)));
            var lazyFields = syntax.Fields.Select(f => new Lazy<Field>(() => new Field(f, syntax, BindType(f.Type!))));

            return new StructType(originalSyntax: syntax,
                                  typeArguments: lazyTypeArguments.ToImmutableArray(),
                                  typeArgumentsNames: typeArgumentsNames.ToImmutableArray(),
                                  inlinedTypes: lazyInlinedTypes.ToImmutableArray(),
                                  fields: lazyFields.ToImmutableArray());
        }


        public IType BindType(ITypeSyntax syntax) {
            return symbols.AlreadyBound(syntax)
                       ? symbols.GetBindingFor(syntax)
                       : symbols.MakeBindingFor(syntax, BindTypeCore(syntax));
        }


        private IType BindTypeCore(ITypeSyntax syntax) {
            if (syntax is AnyTypeSyntax) return new AnyType();
            if (syntax is VoidTypeSyntax) return new VoidType();
            if (syntax is ArrayTypeSyntax arrayType) return new ArrayType(BindType(arrayType.ElementType));
            if (syntax is FunctionPointerTypeSyntax functionPointerType) {
                var returnType = BindType(functionPointerType.ReturnType);
                var parameters = functionPointerType.Parameters.Select(BindType);
                return new FunctionPointerType(returnType, parameters);
            }
            if (syntax is PrimitiveTypeSyntax)
            {
                return syntax.Name switch
                {
                    "string" => new PrimitiveType(PrimitiveKind.STRING),
                    "num" => new PrimitiveType(PrimitiveKind.NUM),
                    "bool" => new PrimitiveType(PrimitiveKind.BOOL),
                    "char" => new PrimitiveType(PrimitiveKind.CHAR),
                    var x => throw new Exception("unsupported primitive type with name: #{x}")
                };
            }
            if (syntax is StructTypeSyntax structTypeSyntax) {
                var matchingBoundTypes = symbols.OpenStructsBinding
                                                .Values
                                                .Where(x => x is StructType type
                                                         && type.NonGenericName == syntax.Name
                                                         && type.Arity == structTypeSyntax.Arity
                                                         && type.Abstraction <= structTypeSyntax.Abstraction)
                                                .Cast<StructType>()
                                                .ToList();

                //check for missing definition
                if (matchingBoundTypes.None()) {
                    errors.Add(new UseOfUndeclaredType(syntax.File, syntax.Line, syntax.FullName));
                    return new UndeclaredStructType(syntax.FullName);
                }

                //check for multiple definitions
                if (matchingBoundTypes.MoreThenOne()) {
                    errors.Add(new UseOfAmbiguousType(syntax.File, syntax.Line, syntax.FullName));
                    return new UndeclaredStructType(syntax.FullName);
                }

                var openType = matchingBoundTypes.First();

                //check if we need to perform type substitution
                if (structTypeSyntax.Arity == 0) return openType;

                //prepare a typeMap
                var typeArgumentsTypes = structTypeSyntax.TypeArguments.Select(BindType).ToList();
                var typeParametersTypes = openType.OriginalDefinitionSyntax
                                                  .TypeParameters
                                                  .Select(BindType)
                                                  .OfType<TypeParameterType>()
                                                  .ToList();

                var typeMap = new HashSet<Substitution>();
                for (var i = 0; i < typeParametersTypes.Count; i++) {
                    typeMap.Add(new Substitution(typeParametersTypes[i], typeArgumentsTypes[i]));
                }

                return openType.WithSubstitutedTypes(typeMap);
            }
            if (syntax is TypeParameterSyntax typeParameter) {

                //check for undeclared type parameters
                var parentDeclaration = GetParentDeclarationFor(typeParameter);

                if (parentDeclaration?.IsGeneric == true) {
                    var isDeclared = parentDeclaration.TypeParameters.Any(x => x.Name == typeParameter.Name);
                    if (isDeclared) {

                        if (parentDeclaration is StructDefinitionSyntax) {
                            return new TypeParameterType(typeParameter.Name, parentDeclaration, ImmutableArray<Lazy<TypeParameterType>>.Empty);
                        }
                        if (parentDeclaration is FunctionDefinitionSyntax function) {

                            var validConstraints = function.Constraints.Where(x => x.constrained is TypeParameterSyntax
                                                                                && x.constrain is TypeParameterSyntax
                                                                                && x.constrained.FullName == typeParameter.FullName);
                            return new TypeParameterType(name: typeParameter.FullName,
                                                         originalDeclarer: function,
                                                         constraints: validConstraints.Select(x => new Lazy<TypeParameterType>(() => (TypeParameterType) BindType(x.constrain)))
                                                                                      .ToImmutableArray());
                        }
                    }
                }
                errors.Add(new UseOfUndeclaredType(typeParameter.File, typeParameter.Line, typeParameter.FullName));
                return new UndeclaredStructType(typeParameter.FullName);
            }

            throw new Exception("WTF");
        }


        private bool IsAcyclic(StructDefinitionSyntax definition, ISet<StructDefinitionSyntax> visitedDefinitions) {

            if (visitedDefinitions.Contains(definition)) return false;
            visitedDefinitions.Add(definition);

            foreach (var inlinedType in definition.InlinedTypes) {
                if (inlinedType is StructTypeSyntax inlinedStructType) {
                    var inlinedDefinition = GetVisibleDefinitionOrNull(inlinedStructType);
                    if (inlinedDefinition == null) continue;
                    if (IsAcyclic(inlinedDefinition, visitedDefinitions)) {
                        visitedDefinitions.Remove(inlinedDefinition);
                    }
                    else return false;
                }
            }
            return true;
        }

    }

}