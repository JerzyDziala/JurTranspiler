using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

    public class UseOfAmbiguousType : SingleLocationError{

		public UseOfAmbiguousType(string file, int line, string name) : base(file, line) {
			this.name = name;
		}


		private string name;

		protected override string MessageBody => $"Name: {name}";

    }

}