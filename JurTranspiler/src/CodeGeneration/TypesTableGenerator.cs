using System;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.CodeGeneration {

    public class TypesTableGenerator {

        private Knowledge knowledge { get; }


        public TypesTableGenerator(Knowledge knowledge) {
            this.knowledge = knowledge;
        }


        public string GenerateTypesTable() {

            var typesToGenerate = knowledge.AllTypes
                                           .OfType<StructType>()
                                           .Where(x => x.PreSubstitutionType != null)
                                           .SelectMany(x => knowledge.Fields[x].Select(f => f.Type))
                                           .Concat(knowledge.AllTypes)
                                           .Distinct(Comparer<Type>.MakeComp((a, b) => a.Name == b.Name, x => x.Name.GetHashCode()))
                                           .ToImmutableArray();

            return "const types = {" + typesToGenerate.Select(GenerateTypeTableEntry).Glue(",\n\n") + "};";
        }


        private string GenerateTypeTableEntry(Type type) {
            return type.Name.AsString() + ":" + GenerateTypeInfo(type);
        }


        private string GenerateTypeInfo(Type type) {
            var typeInfoTypeName = type.GetType().Name;
            return $"new {typeInfoTypeName}({GenerateArgumentsForTypeInfo((dynamic) type)})".WithTypeName(typeInfoTypeName);
        }


        private string GenerateArgumentsForTypeInfo(PrimitiveType type) {
            var name = type.Name.AsString();
            return name;
        }


        private string GenerateArgumentsForTypeInfo(ArrayType type) {
            var name = type.Name.AsString();
            var lazyElementType = $"() => types[{type.ElementType.Name.AsString()}]".WithTypeName("Type()");
            return "\n" + name + ",\n" + lazyElementType;
        }


        private string GenerateArgumentsForTypeInfo(FunctionPointerType type) {
            var name = type.Name.AsString();
            var lazyReturnType = $"() => types[{type.ReturnType.Name.AsString()}]".WithTypeName("Type()");
            var lazyParametersTypes = ($"() => " + $"[{type.Parameters.Select(x => $"types[{x.Name.AsString()}]").Glue(", ")}]".WithTypeName("Type[]")).WithTypeName("Type[]()");
            return "\n" + name + ",\n" + lazyReturnType + ",\n" + lazyParametersTypes;
        }


        private string GenerateArgumentsForTypeInfo(StructType type) {
            var name = type.Name.AsString();
            var nonGenericName = type.NonGenericName.AsString();
            var isGeneric = type.IsGeneric ? "true" : "false";
            var lazyTypeArguments = ("() => " + ("[" + type.TypeArguments.Select(x => "types[" + x.Value.Name.AsString() + "]").Glue(", ") + "]").WithTypeName("Type[]")).WithTypeName("Type[]()");
            var lazyFields = ("() => " + ("[" + knowledge.Fields[type].Select(x => "new Field(" + x.Name.AsString() + ", " + "types[" + x.Type.Name.AsString() + "])").Glue(", ") + "]").WithTypeName("Field[]")).WithTypeName("Field[]()");
            var lazyInlinedTypes = ("() => " + ("[" + type.InlinedTypes.Select(x => "types[" + x.Value.Name.AsString() + "]").Glue(", ") + "]").WithTypeName("Type[]")).WithTypeName("Type[]()");

            return "\n" + name + ",\n" + nonGenericName + ",\n" + isGeneric + ",\n" + lazyTypeArguments + ",\n" + lazyFields + ",\n" + lazyInlinedTypes;
        }


        private string GenerateArgumentsForTypeInfo(TypeParameterType type) {
            var name = type.Name.AsString();
            return name;
        }


        private string GenerateArgumentsForTypeInfo(VoidType type) {
            return "";
        }

        private string GenerateArgumentsForTypeInfo(AnyType type) {
            return "";
        }
    }

}