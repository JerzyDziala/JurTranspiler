using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Xml;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.src.syntax_tree.types;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.semantic_model {

    public class StructType : Type, IEquatable<StructType> {

        public override ImmutableList<ITreeNode> ImmediateChildren { get; }
        public override ImmutableList<ITreeNode> AllChildren { get; }
        public override string Name { get; }

        public string NonGenericName { get; }
        public bool IsGeneric { get; }
        public StructDefinitionSyntax OriginalDefinitionSyntax { get; }
        public int Arity { get; }
        public int Abstraction { get; }

        public ImmutableList<Lazy<Type>> TypeArguments { get; }
        public ImmutableList<string> TypeArgumentsNames { get; }
        public ImmutableList<Lazy<Field>> Fields { get; }
        public ImmutableList<Lazy<Type>> InlinedTypes { get; }
        public StructType PreSubstitutionType { get; }

        public string JsName => Name.Replace("<", "_S_").Replace(">", "_E_").Replace(",", "___");


        public StructType(StructDefinitionSyntax originalSyntax,
                          ImmutableList<Lazy<Type>> typeArguments,
                          ImmutableList<string> typeArgumentsNames,
                          ImmutableList<Lazy<Type>> inlinedTypes,
                          ImmutableList<Lazy<Field>> fields,
                          StructType preSubstitutionType = null) {
            OriginalDefinitionSyntax = originalSyntax;
            NonGenericName = originalSyntax.Name;
            TypeArguments = typeArguments;
            TypeArgumentsNames = typeArgumentsNames;
            InlinedTypes = inlinedTypes;
            Fields = fields;
            PreSubstitutionType = preSubstitutionType;
            IsGeneric = TypeArguments.Count > 0;
            Arity = TypeArguments.Count;
            Abstraction = OriginalDefinitionSyntax.Abstraction;

            Name = NonGenericName;
            if (IsGeneric) {
                Name += "<" + string.Join(",", typeArgumentsNames) + ">";
            }

            ImmediateChildren = ImmutableList.Create<ITreeNode>();
            AllChildren = this.GetAllChildren();
        }


        public override Type WithSubstitutedTypes(ISet<Substitution> typeMap) {

            var typeArguments = TypeArguments.Select(arg => new Lazy<Type>(() => typeMap.FirstOrDefault(sub => sub.typeParameter.Equals(arg.Value))?.typeArgument ?? arg.Value)).ToImmutableList();

            return new StructType(originalSyntax: OriginalDefinitionSyntax,
                                  typeArguments: typeArguments,
                                  typeArgumentsNames: typeArguments.Select(x => x.Value.Name).ToImmutableList(),
                                  inlinedTypes: InlinedTypes.Select(type => new Lazy<Type>(() => type.Value.WithSubstitutedTypes(typeMap))).ToImmutableList(),
                                  fields: Fields.Select(field => new Lazy<Field>(() => new Field(originalSyntax: field.Value.OriginalSyntax,
                                                                                                 originalOwnerSyntax: field.Value.OriginalOwnerSyntax,
                                                                                                 type: field.Value.Type.WithSubstitutedTypes(typeMap)))).ToImmutableList(),
                                  this);
        }


        public bool Equals(StructType other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name)
                && TypeArguments.Select(x => x.Value).SequenceEqual(other.TypeArguments.Select(x => x.Value))
                && OriginalDefinitionSyntax.Equals(other.OriginalDefinitionSyntax);
        }


        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((StructType) obj);
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