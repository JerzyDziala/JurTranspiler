using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.CodeGeneration {

    public class Generator {

        private Knowledge knowledge { get; }
        private SyntaxTree syntaxTree { get; }


        public Generator(Knowledge knowledge, SyntaxTree syntaxTree) {
            this.knowledge = knowledge;
            this.syntaxTree = syntaxTree;
        }


        public string GenerateJs() {
            return $@"//Program//

//---Types Table---//

{GenerateTypesTable()}

//---internal javascript functions---//

{JsStrings.TypeOfDefinition}
{JsStrings.GetTypeDefinition}
{JsStrings.WithTypeNameDefinition}

//---internal javascript constructors---//

{JsStrings.TypeInfoConstructorDefinition}
{JsStrings.FieldInfoConstructorDefinition}

//---User's code---//
{GenerateConstructors()}
{syntaxTree.ToJs(knowledge)}
";
        }


        public string GenerateTypesTable() {
            Func<string, string, string> withTypeName = (obj, name) => $"withTypeName({obj},'{name}')";
            Func<string, string> newTypeInfo = args => $"new TypeInfo(\n{args})";
            Func<string, string, string> typeTableEntry = (typeName, entry) => $"'{typeName}':{entry}";

            Func<string, string, string> createEntry = (entryName, args) => typeTableEntry(entryName, withTypeName(newTypeInfo(args), "TypeInfo"));

            Func<Type, string> getElementTypeString = type => type is ArrayType arrayType ? $"() => types['{arrayType.ElementType.Name}']" : "() => null";
            Func<Type, string> getIsArraySting = type => type is ArrayType ? "true" : "false";

            Func<string, string> asFieldInfoConstructor = args => $"new FieldInfo({args})";
            Func<string, string> asFieldInfoConstructorWithTypeName = args => withTypeName(asFieldInfoConstructor(args), "FieldInfo");
            Func<Field, string> asFieldInfoArgs = field => $"'{field.Name}', types['{field.Type.Name}']";

            Func<Field, string> asFieldInfoWithTypeName = field => asFieldInfoConstructorWithTypeName(asFieldInfoArgs(field));

            Func<ImmutableArray<Field>, string> getFields = fields => $"() => [\n{fields.Select(asFieldInfoWithTypeName).Glue(",\n")}]";
            Func<Type, ImmutableArray<Field>> getAllFields = type => type is StructType structType ? knowledge.Fields[structType] : ImmutableArray.Create<Field>();

            Func<Type, string> getFieldsString = type => getFields(getAllFields(type));

            Func<Type, string> createArgs = type => $"'{type.Name}',\n{getIsArraySting(type)},\n {getElementTypeString(type)},\n {getFieldsString(type)}";
            Func<Type, string> asEntry = type => createEntry(type.Name, createArgs(type));

            Func<string> createTypesTable = () => $"const types = {{\n{knowledge.AllTypes.Distinct(UtilityLibrary.Comparer<Type>.MakeComp((a, b) => a.Name == b.Name, x => x.Name.GetHashCode())).Select(asEntry).Glue(",\n\n")}}};";


            return createTypesTable();
        }


        public string GenerateConstructors() {
            var structs = knowledge.AllTypes
                                   .OfType<StructType>()
                                   .Distinct(UtilityLibrary.Comparer<StructType>.MakeComp((a, b) => a.Name == b.Name, x => x.Name.GetHashCode()))
                                   .Where(x => !x.isExtern)
                                   .ToImmutableArray();

            Func<Field, string> fieldDefinitionGenerator = field => $"this.{field.Name} = {field.Type.GetDefaultValue()};";

            Func<StructType, string> constructorFunctionGenerator = type => $@"function {type.JsName}(){{
                            {knowledge.Fields[type].Select(fieldDefinitionGenerator).Glue("\n")}
                        }}";

            return structs.Select(constructorFunctionGenerator).Glue("\n\n");
        }

    }

}