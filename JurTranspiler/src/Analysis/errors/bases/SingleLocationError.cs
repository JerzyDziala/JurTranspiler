namespace JurTranspiler.compilerSource.Analysis {

	public abstract class SingleLocationError : Error {

		protected string file { get; }
		protected int line { get; }


		protected SingleLocationError(string file, int line) {
			this.file = file;
			this.line = line;
		}


		protected string GetLocationString => $"File: {file}, Line: {line}";

	}

}