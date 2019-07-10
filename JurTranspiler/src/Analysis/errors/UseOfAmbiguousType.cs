using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

    public class UseOfAmbiguousType : SingleLocationError{

		public UseOfAmbiguousType(string file, int line, string name) : base(file, line) {
			this.name = name;
		}


		private string name;

		public override string GetMessage() => $"UseOfAmbiguousType ### {GetLocationString}, Name: {name}";
    }

}