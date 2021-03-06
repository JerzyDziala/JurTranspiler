using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using JurTranspiler.syntax_tree.declarations;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.semantic_model.types {

    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class StructType : Type, IEquatable<StructType> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

        public override string Name => IsGeneric
                                           ? NonGenericName + "<" + string.Join(",", TypeArgumentsNames) + ">"
                                           : NonGenericName;

        public StructDefinitionSyntax OriginalDefinitionSyntax { get; }
        public string NonGenericName => OriginalDefinitionSyntax.Name;
        public bool IsNominal => OriginalDefinitionSyntax.IsNominal || InlinedTypes.OfType<Lazy<StructType>>().Any(x=>x.Value.IsNominal);
        public int Arity => TypeArguments.Length;
        public bool IsGeneric => Arity > 0;
        public int Abstraction => OriginalDefinitionSyntax.Abstraction;
        public ImmutableArray<Lazy<IType>> TypeArguments { get; }
        public ImmutableArray<string> TypeArgumentsNames { get; }
        public ImmutableArray<Lazy<Field>> Fields { get; }
        public ImmutableArray<Lazy<IType>> InlinedTypes { get; }
        public StructType? PreSubstitutionType { get; }


        public StructType(StructDefinitionSyntax originalSyntax,
                          ImmutableArray<Lazy<IType>> typeArguments,
                          ImmutableArray<string> typeArgumentsNames,
                          ImmutableArray<Lazy<IType>> inlinedTypes,
                          ImmutableArray<Lazy<Field>> fields,
                          StructType? preSubstitutionType = null) {
            OriginalDefinitionSyntax = originalSyntax;
            TypeArguments = typeArguments;
            TypeArgumentsNames = typeArgumentsNames;
            InlinedTypes = inlinedTypes;
            Fields = fields;
            PreSubstitutionType = preSubstitutionType;

            ImmediateChildren = ImmutableArray.Create<ITreeNode>();

        }

        public override IType WithSubstitutedTypes(ISet<Substitution> typeMap) {

            var typeArguments = TypeArguments.Select(x => new Lazy<IType>(() => x.Value.WithSubstitutedTypes(typeMap))).ToImmutableArray();

            return new StructType(originalSyntax: OriginalDefinitionSyntax,
                                  typeArguments: typeArguments,
                                  typeArgumentsNames: typeArguments.Select(x => x.Value.Name).ToImmutableArray(),
                                  inlinedTypes: InlinedTypes.Select(type => new Lazy<IType>(() => type.Value.WithSubstitutedTypes(typeMap))).ToImmutableArray(),
                                  fields: Fields.Select(field => new Lazy<Field>(() => new Field(originalSyntax: field.Value.OriginalSyntax,
                                                                                                 originalOwnerSyntax: field.Value.OriginalOwnerSyntax,
                                                                                                 type: field.Value.Type.WithSubstitutedTypes(typeMap)))).ToImmutableArray(),
                                  this);
        }


        public bool Equals(StructType other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name)
                && TypeArguments.Select(x => x.Value).SequenceEqual(other.TypeArguments.Select(x => x.Value))
                && OriginalDefinitionSyntax.Equals(other.OriginalDefinitionSyntax);
        }


        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj?.GetType() != GetType()) return false;
            return Equals((StructType) obj!);
        }


        public override int GetHashCode() {
            unchecked {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                foreach (var typeArgument in TypeArguments.Select(x => x.Value)) {
                    hashCode = (hashCode * 397) ^ typeArgument.GetHashCode();
                }
                hashCode ^= OriginalDefinitionSyntax.GetHashCode();
                return hashCode;
            }
        }

    }

}