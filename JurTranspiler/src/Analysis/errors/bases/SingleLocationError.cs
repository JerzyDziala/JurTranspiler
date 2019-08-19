namespace JurTranspiler.compilerSource.Analysis {

	public abstract class SingleLocationError : Error {

		protected override string Localization => $"file: {file}line: {line}";
		protected string file { get; }
		protected int line { get; }


		protected SingleLocationError(string file, int line) {
			this.file = file;
			this.line = line;
		}

	}

}