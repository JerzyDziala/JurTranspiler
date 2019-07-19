using System.Collections.Generic;
using System.Linq;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class Binder {

        public IEnumerable<Field> BindFields(StructType type) {
            return symbols.AlreadyBound(type)
                       ? symbols.GetBindingFor(type)
                       : symbols.MakeBindingFor(type, BindFieldsCore(type));
        }


        private ICollection<Field> BindFieldsCore(StructType type) {
            //yes, we merge fields that have the same type and name. this makes multiple inheritance easier for the user
            var allFields = new HashSet<Field>(type.Fields.Select(lazy => lazy.Value));

            //fields with the same names are errors
            var duplicates = allFields.GroupBy(field => field.Name).Where(group => group.Count() > 1);

            //Error: DuplicateFieldsNames
            foreach (var duplicateGroup in duplicates) {
                errors.Add(new MultipleFieldsWithTheSameName(duplicateGroup.Select(field => (field.OriginalFile, field.OriginalLine)), duplicateGroup.First().Name));
            }

            foreach (var inlinedType in type.InlinedTypes.Select(x => x.Value).ToList()) {
                if (inlinedType is StructType s) allFields.AddRange(BindFields(s));
            }

            var duplicatesAfterInlining = allFields.GroupBy(field => field.Name)
                                                   .Where(group => group.Count() > 1);

            foreach (var duplicateGroup in duplicatesAfterInlining) {

                //we already checked our fields for duplicates.
                if (duplicateGroup.All(field => type.Fields.Select(lazyF => lazyF.Value.OriginalOwnerSyntax).Contains(field.OriginalOwnerSyntax))) continue;

                //we report all the cases that were caused by inlining
                errors.Add(new InliningResultsInMultipleFieldsWithTheSameName(type.OriginalDefinitionSyntax.File,
                                                                              type.OriginalDefinitionSyntax.Line,
                                                                              duplicateGroup.First().Name,
                                                                              duplicateGroup.Select(field => field.OriginalOwnerSyntax.FullName).Distinct()));
            }
            return allFields;
        }
    }

}