using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

    public class CouldNotResolveAmbiguousFunctionCall : SingleLocationError{

		private string name { get; }

		public CouldNotResolveAmbiguousFunctionCall(string file, int line, string name) : base(file, line) {
			this.name = name;
		}


		protected override string MessageBody => $"Name: {name}";

    }

}