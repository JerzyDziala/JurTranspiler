using System;
using System.Collections.Generic;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.CodeGeneration {

    public static class JsStrings {

        public static string WithTypeName(this string body, string name) => $"withTypeName({body},'{name}')";

        public static readonly string Hack = "_$";

        public static string WithTypeNameDefinition =>
            @"function withTypeName(obj,typeName){
                obj.typeName = typeName;
                return obj;
            }";

        public static string TypeOfDefinition =>
            @"function typeOf(obj) {
                const buildInType = {}.toString.call(obj).split(' ')[1].slice(0, -1).toLowerCase();
                return buildInType === 'object' ? obj.constructor.name : buildInType;
            }";

        public static string GetTypeDefinition =>
            @"function getType(obj) {

                const typeName = obj.typeName;

                if (typeName === undefined || typeName === null) {

                    const jsTypeName = typeOf(obj);
                    const jurType = types[jsTypeName];

                    return jurType === undefined || jurType === null
                        ? null
                        : jurType;
                }

                return types[typeName];
            }";

        public static string TypeInfoConstructorDefinition => @"
            function TypeInfo(name, isArray, elementType, fields) {
                this.name = name;
                this.isArray = isArray;
                this.getElementType = elementType;
                this.getFields = fields;
            }";

        public static string FieldInfoConstructorDefinition => @"
            function FieldInfo(name, fieldType) {
                this.name = name;
                this.fieldType = fieldType;
            }";
    }

}