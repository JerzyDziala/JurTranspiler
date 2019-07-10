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

		public StructType PreSubstitutionType { get; }
		public ImmutableList<Lazy<Type>> TypeArguments { get; }
		public ImmutableList<Lazy<Field>> Fields { get; }
		public ImmutableList<Lazy<Type>> InlinedTypes { get; }

		//memoized version of GetAllFieldsCore
		private Func<HashSet<Error>, IEnumerable<Field>> getAllFields;
		public Func<HashSet<Error>, IEnumerable<Field>> GetAllFields {
			get {
				if (getAllFields == null) {
					getAllFields = MemoizeGetAllFields(GetAllFieldsCore);
					return getAllFields;
				}
				else return getAllFields;
			}
		}


		public StructType(StructDefinitionSyntax originalSyntax,
		                  ImmutableList<Lazy<Type>> typeArguments,
		                  ImmutableList<string> typeArgumentsNames,
		                  ImmutableList<Lazy<Type>> inlinedTypes,
		                  ImmutableList<Lazy<Field>> fields,
		                  StructType preSubstitutionType = null) {
			OriginalDefinitionSyntax = originalSyntax;
			PreSubstitutionType = preSubstitutionType;
			NonGenericName = originalSyntax.Name;
			TypeArguments = typeArguments;
			InlinedTypes = inlinedTypes;
			Fields = fields;
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


		private static Func<HashSet<Error>, IEnumerable<Field>> MemoizeGetAllFields(Func<HashSet<Error>, IEnumerable<Field>> func) {
			IEnumerable<Field> cache = null;
			return errors => cache ?? (cache = func(errors));
		}


		protected override bool IsAssignableToCore(Type type, HashSet<Error> errors) {
			if (type is UndefinedType) return true;
			if (type is StructType target) {
				var fieldsA = GetAllFields(errors);
				var fieldsB = target.GetAllFields(errors);
				return fieldsB.All(fieldB => fieldsA.Any(fieldA => fieldA.Name == fieldB.Name
				                                                && fieldA.Type.Equals(fieldB.Type)));
			}
			return false;
		}


		public override bool IsAssignableToWithSubstitutions(Type type, ICollection<Substitution> substitutions, HashSet<Error> errors) {
			if (type is UndefinedType) return true;
			if (type is TypeParameterType t) {
				substitutions.Add(new Substitution(t, this));
				return true;
			}
			if (type is StructType target) {
				var fieldsA = GetAllFields(errors).ToList();
				var fieldsB = target.GetAllFields(errors);
				var tmpSubs = new HashSet<Substitution>();

				var isCompatible = fieldsB.All(fieldB => fieldsA.Any(fieldA => fieldA.Type.IsEqualToWithSubstitutions(fieldB.Type, tmpSubs, errors)
				                                                            && fieldA.Name.Equals(fieldB.Name)));

				if (!isCompatible) return false;
				else if (tmpSubs.GroupBy(x => x.typeParameter).Any(g => g.MoreThenOne())) return false;
				else {
					substitutions.AddRange(tmpSubs);
					return true;
				}
			}
			return false;
		}


		public override bool IsEqualToWithSubstitutions(Type type, ICollection<Substitution> substitutions, HashSet<Error> errors) {
			if (type is TypeParameterType t) {
				substitutions.Add(new Substitution(t, this));
				return true;
			}
			if (type is StructType target) {
				var fieldsA = GetAllFields(errors).ToList();
				var fieldsB = target.GetAllFields(errors).ToList();
				var tmpSubs = new HashSet<Substitution>();

				if (fieldsA.Count == fieldsB.Count) {
					var isCompatible = fieldsB.All(fieldB => fieldsA.Any(fieldA => fieldA.Name.Equals(fieldB.Name)
					                                                            && fieldA.Type.IsEqualToWithSubstitutions(fieldB.Type, tmpSubs, errors)));

					if (!isCompatible) return false;
					else if (tmpSubs.GroupBy(x => x.typeParameter).Any(g => g.MoreThenOne())) return false;
					else {
						substitutions.AddRange(tmpSubs);
						return true;
					}
				}
				return false;
			}
			return false;
		}


		public IEnumerable<Field> GetAllCachedFields() => GetAllFields(new HashSet<Error>());


		private IEnumerable<Field> GetAllFieldsCore(HashSet<Error> errors) {
			//yes, we merge fields that have the same type and name. this makes multiple inheritance easier for the user
			var allFields = new HashSet<Field>(Fields.Select(lazy => lazy.Value));

			//fields with the same names are errors
			var duplicates = allFields.GroupBy(field => field.Name).Where(group => group.Count() > 1);

			//Error: DuplicateFieldsNames
			foreach (var duplicateGroup in duplicates) {
				errors.Add(new MultipleFieldsWithTheSameName(duplicateGroup.Select(field => (field.OriginalFile, field.OriginalLine)), duplicateGroup.First().Name));
			}

			foreach (var inlinedType in InlinedTypes.Select(x => x.Value).ToList()) {
				if (inlinedType is StructType s) allFields.AddRange(s.GetAllFields(errors));
			}

			var duplicatesAfterInlining = allFields.GroupBy(field => field.Name)
			                                       .Where(group => group.Count() > 1);

			foreach (var duplicateGroup in duplicatesAfterInlining) {

				//we already checked our fields for duplicates.
				if (duplicateGroup.All(field => this.Fields.Select(lazyF => lazyF.Value.OriginalOwnerSyntax).Contains(field.OriginalOwnerSyntax))) continue;

				//we report all the cases that were caused by inlining
				errors.Add(new InliningResultsInMultipleFieldsWithTheSameName(OriginalDefinitionSyntax.File,
				                                                              OriginalDefinitionSyntax.Line,
				                                                              duplicateGroup.First().Name,
				                                                              duplicateGroup.Select(field => field.OriginalOwnerSyntax.FullName).Distinct()));
			}
			return allFields;
		}


		public override Type WithSubstitutedTypes(ISet<Substitution> typeMap) {
			return new StructType(originalSyntax: OriginalDefinitionSyntax,
			                      typeArguments: typeMap.Select(substitution => new Lazy<Type>(() => substitution.typeArgument)).ToImmutableList(),
			                      typeArgumentsNames: typeMap.Select(substitution => substitution.typeArgument.Name).ToImmutableList(),
			                      inlinedTypes: InlinedTypes.Select(type => new Lazy<Type>(() => type.Value.WithSubstitutedTypes(typeMap))).ToImmutableList(),
			                      fields: Fields.Select(field => new Lazy<Field>(() => new Field(originalSyntax: field.Value.OriginalSyntax,
			                                                                                     originalOwnerSyntax: field.Value.OriginalOwnerSyntax,
			                                                                                     type: field.Value.Type.WithSubstitutedTypes(typeMap)))).ToImmutableList(),
			                      this);
		}


		public override string GetJsTypeCacheGetter() {

			var fieldsJs = GetAllCachedFields()
			               .Select(x => $"{{name: '{x.Name}', type: {x.Type.GetJsTypeCacheGetter()}}}")
			               .Glue(",\n");
			return $"getCachedStructType$('{Name}',() => [{fieldsJs}])";
		}


		public bool Equals(StructType other) {
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Name, other.Name)
			    && string.Equals(NonGenericName, other.NonGenericName)
			    && TypeArguments.SequenceEqual(other.TypeArguments)
			    && Fields.SequenceEqual(other.Fields)
			    && InlinedTypes.SequenceEqual(other.InlinedTypes);
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
				hashCode = (hashCode * 397) ^ (NonGenericName != null ? NonGenericName.GetHashCode() : 0);
				foreach (var typeArgument in TypeArguments) {
					hashCode = (hashCode * 397) ^ typeArgument.GetHashCode();
				}
				hashCode ^= OriginalDefinitionSyntax.File.GetHashCode() ^ OriginalDefinitionSyntax.Line.GetHashCode();
				return hashCode;
			}
		}

	}

}