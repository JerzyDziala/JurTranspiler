using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using JurTranspiler.src.syntax_tree.types;
using UtilityLibrary;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {

        public Type BindStructDefinition(StructDefinitionSyntax syntax) {
            return symbols.AlreadyBound(syntax)
                       ? symbols.GetBindingFor(syntax)
                       : symbols.MakeBindingFor(syntax, BindStructDefinitionCore(syntax));
        }


        private Type BindStructDefinitionCore(StructDefinitionSyntax syntax) {

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
                errors.Add(new MultipleTypeParametersWithTheSameName(group.GetLocations(), group.First().FullName));
            }
            if (duplicates.Any()) return new UndeclaredStructType(syntax.FullName);

            //we want the construction of typeArguments to happen lazily only after the struct constructor has run
            //but to create a FullName for the StructType we need to eagerly load their names
            var typeArgumentsNames = syntax.TypeParameters.Select(p => p.FullName);
            var lazyTypeArguments = syntax.TypeParameters.Select(p => new Lazy<Type>(() => BindType(p)));

            var lazyInlinedTypes = hasCycles ? new Lazy<Type>[0] : validInlines.Select(inline => new Lazy<Type>(() => BindType(inline)));
            var lazyFields = syntax.Fields.Select(f => new Lazy<Field>(() => new Field(f, syntax, BindType(f.Type))));

            return new StructType(originalSyntax: syntax,
                                  typeArguments: lazyTypeArguments.ToImmutableList(),
                                  typeArgumentsNames: typeArgumentsNames.ToImmutableList(),
                                  inlinedTypes: lazyInlinedTypes.ToImmutableList(),
                                  fields: lazyFields.ToImmutableList());
        }


        public Type BindType(ITypeSyntax syntax) {
            return symbols.AlreadyBound(syntax)
                       ? symbols.GetBindingFor(syntax)
                       : symbols.MakeBindingFor(syntax, BindTypeCore(syntax));
        }


        private Type BindTypeCore(ITypeSyntax syntax) {
            if (syntax is VoidTypeSyntax) return new VoidType();
            if (syntax is ArrayTypeSyntax arrayType) return new ArrayType(BindType(arrayType.ElementType));
            if (syntax is FunctionPointerTypeSyntax functionPointerType) {
                var returnType = BindType(functionPointerType.ReturnType);
                var parameters = functionPointerType.Parameters.Select(BindType);
                return new FunctionPointerType(returnType, parameters);
            }
            if (syntax is PrimitiveTypeSyntax) {
                if (syntax.Name == "string") return new PrimitiveType(PrimitiveKind.STRING);
                else if (syntax.Name == "num") return new PrimitiveType(PrimitiveKind.NUM);
                else if (syntax.Name == "bool") return new PrimitiveType(PrimitiveKind.BOOL);
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
                    return new UndeclaredStructType(syntax.Name);
                }

                //check for multiple definitions
                if (matchingBoundTypes.MoreThenOne()) {
                    errors.Add(new UseOfAmbiguousType(syntax.File, syntax.Line, syntax.FullName));
                    return new UndeclaredStructType(syntax.Name);
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
                for (int i = 0; i < typeParametersTypes.Count; i++) {
                    typeMap.Add(new Substitution(typeParametersTypes[i], typeArgumentsTypes[i]));
                }

                return openType.WithSubstitutedTypes(typeMap);
            }
            if (syntax is TypeParameterSyntax typeParameter) {

                //check for undeclared type parameters
                var parentDeclaration = symbols.GetParentDeclarationFor(typeParameter);

                if (parentDeclaration?.IsGeneric == true) {
                    var isDeclared = parentDeclaration.TypeParameters.Any(x => x.Name == typeParameter.Name);
                    if (isDeclared) {

                        if (parentDeclaration is StructDefinitionSyntax) {
                            return new TypeParameterType(typeParameter.Name, parentDeclaration, ImmutableList<Lazy<TypeParameterType>>.Empty);
                        }
                        if (parentDeclaration is FunctionDefinitionSyntax function) {

                            var validConstraints = function.Constraints.Where(x => x.constrained is TypeParameterSyntax
                                                                                && x.constrain is TypeParameterSyntax
                                                                                && x.constrained.FullName == typeParameter.FullName);
                            return new TypeParameterType(name: typeParameter.FullName,
                                                         originalDeclarer: function,
                                                         constraints: validConstraints.Select(x => new Lazy<TypeParameterType>(() => (TypeParameterType) BindType(x.constrain)))
                                                                                      .ToImmutableList());
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
                    var inlinedDefinition = symbols.GetVisibleDefinitionOrNull(inlinedStructType);
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