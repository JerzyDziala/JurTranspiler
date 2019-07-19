using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using JurTranspiler.compilerSource;
using UtilityLibrary;

namespace JurTranspiler {

    class Program {
        static void Main(string[] args) {
            var projectDirectory = Environment.CurrentDirectory + "/" + args[0];
            var outputDirectory = Environment.CurrentDirectory + "/" + args[1];

            var projectJsFiles = Directory.GetFiles(projectDirectory, "*.js", SearchOption.AllDirectories);
            var projectJurFiles = Directory.GetFiles(projectDirectory, "*.jur", SearchOption.AllDirectories);

            var jsCode = projectJsFiles.Select(File.ReadAllText).Glue("\n\n");
            var jurCode = projectJurFiles.Select(x => (File.ReadAllText(x), x));

            var (diagnostics, js) = Compiler.Compile(jurCode);

            Console.ForegroundColor = ConsoleColor.Red;
            diagnostics.Select(x => x.GetMessage())
                       .ToList()
                       .ForEach(Console.WriteLine);
            Console.ForegroundColor = ConsoleColor.White;

            File.WriteAllText(outputDirectory + "/output.js", js + "\n" + jsCode);
        }

    }

}