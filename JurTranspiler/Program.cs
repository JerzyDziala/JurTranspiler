using System;
using System.IO;
using System.Linq;
using UtilityLibrary;

namespace JurTranspiler {

	static class Program {

		static int Main(string[] args) {
			var projectDirectory = Environment.CurrentDirectory + "/" + args[0];
			var outputDirectory = Environment.CurrentDirectory + "/" + args[1];

			var projectJsFiles = Directory.GetFiles(projectDirectory, "*.js", SearchOption.AllDirectories);
			var projectJurFiles = Directory.GetFiles(projectDirectory, "*.jur", SearchOption.AllDirectories);

			var jsCode = projectJsFiles.Select(File.ReadAllText).Glue("\n\n");
			var jurCode = projectJurFiles.Select(x => (File.ReadAllText(x), x));

			var (diagnostics, js) = Compiler.Compile(jurCode);

			Console.ForegroundColor = ConsoleColor.Red;
			diagnostics.Select(x => x.ToString())
			           .ToList()
			           .ForEach(Console.Write);
			Console.ForegroundColor = ConsoleColor.White;

			if (diagnostics.Any())
				return -1;

			File.WriteAllText(outputDirectory + "/output.js", js + "\n" + jsCode);

			return 0;
		}

	}

}