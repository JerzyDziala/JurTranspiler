using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.CodeGeneration {

    public class TypesTableGenerator {

        private Knowledge knowledge { get; }


        public TypesTableGenerator(Knowledge knowledge) {
            this.knowledge = knowledge;
        }


        public string GenerateTypesTable() {

            var typesToGenerate = knowledge.AllTypes.Where(x => !(x is NullType));

            return "const _t_ = {" + typesToGenerate.Select(GenerateTypeTableEntry).Glue(",\n\n") + "};";
        }


        private string GenerateTypeTableEntry(IType type) {
            return type.Name.AsString() + ":" + GenerateTypeInfo(type);
        }


        private string GenerateTypeInfo(IType type) {
            var typeInfoTypeName = type.GetType().Name;
            return $"new {typeInfoTypeName}({GenerateArgumentsForTypeInfo((dynamic) type)})".WithTypeName(typeInfoTypeName);
        }


        private string GenerateArgumentsForTypeInfo(PrimitiveType type) {
            var name = type.Name.AsString();
            return name;
        }


        private string GenerateArgumentsForTypeInfo(ArrayType type) {
            var name = type.Name.AsString();
            var lazyElementType = $"() => _t_[{type.ElementType.Name.AsString()}]".WithTypeName("Type()");
            return "\n" + name + ",\n" + lazyElementType;
        }


        private string GenerateArgumentsForTypeInfo(FunctionPointerType type) {
            var name = type.Name.AsString();
            var lazyReturnType = $"() => _t_[{type.ReturnType.Name.AsString()}]".WithTypeName("Type()");
            var lazyParametersTypes = ($"() => " + $"[{type.Parameters.Select(x => $"_t_[{x.Name.AsString()}]").Glue(", ")}]".WithTypeName("Type[]")).WithTypeName("Type[]()");
            return "\n" + name + ",\n" + lazyReturnType + ",\n" + lazyParametersTypes;
        }


        private string GenerateArgumentsForTypeInfo(StructType type) {
            var name = type.Name.AsString();
            var nonGenericName = type.NonGenericName.AsString();
            var isGeneric = type.IsGeneric ? "true" : "false";
            var lazyTypeArguments = ("() => " + ("[" + type.TypeArguments.Select(x => "_t_[" + x.Value.Name.AsString() + "]").Glue(", ") + "]").WithTypeName("Type[]")).WithTypeName("Type[]()");
            var lazyFields = ("() => " + ("[" + knowledge.Fields[type].Select(x => "new Field(" + x.Name.AsString() + ", " + "_t_[" + x.Type.Name.AsString() + "])").Glue(", ") + "]").WithTypeName("Field[]")).WithTypeName("Field[]()");
            var lazyInlinedTypes = ("() => " + ("[" + type.InlinedTypes.Select(x => "_t_[" + x.Value.Name.AsString() + "]").Glue(", ") + "]").WithTypeName("Type[]")).WithTypeName("Type[]()");

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