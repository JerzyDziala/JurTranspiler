using System;
using System.Collections.Generic;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.CodeGeneration {

    public static class JsStrings {

        public static string WithTypeName(this string body, string name) => $"withTypeName({body},'{name}')";
        public static string WithTypeNameNoParents(this string body, string name) => $"withTypeName({body},{name})";

    }

}