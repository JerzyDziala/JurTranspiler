using System.Collections.Generic;
using UtilityLibrary;

namespace JurTranspiler.CodeGeneration {

    public static class GenerationUtils {
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