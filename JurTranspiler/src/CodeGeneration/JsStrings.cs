using System.Collections.Generic;
using UtilityLibrary;

namespace JurTranspiler.CodeGeneration {

	public static class JsStrings {

		public static string WithTypeName(this string body, string name) => $"_wtn_({body},'{name}')";
		public static string WithTypeNameNoParents(this string body, string name) => $"_wtn_({body},{name})";
		public static string AsSingleLineLambda(this string body, params string[] args) => $"({args.Glue(", ")}) => {body}";

        public static string AsString(this string text) {
            return $"'{text}'";
        }


        public static string AsArray(this string text) {
            return $"[{text}]";
        }

        public static string AsObject(this string text) {
            return $"{{{text}}}";
        }

        public static string AsArray(this IEnumerable<string> text) {
            return $"[{text.Glue(", ")}]";
        }
	}

}