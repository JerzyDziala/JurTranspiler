using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

    public class CouldNotResolveAmbiguousFunctionCall : SingleLocationError{

		public CouldNotResolveAmbiguousFunctionCall(string file, int line, string name) : base(file, line) {
			this.name = name;
		}


		private string name;

		public override string GetMessage() => $"CouldNotResolveAmbiguousFunctionCall ### {GetLocationString}, Name: {name}";
    }

}