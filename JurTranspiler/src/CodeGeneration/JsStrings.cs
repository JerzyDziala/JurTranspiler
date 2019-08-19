namespace JurTranspiler.compilerSource.CodeGeneration {

    public static class JsStrings {

        public static string WithTypeName(this string body, string name) => $"_wtn_({body},'{name}')";
        public static string WithTypeNameNoParents(this string body, string name) => $"_wtn_({body},{name})";

    }

}