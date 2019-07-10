namespace JurTranspiler.compilerSource.Analysis {

	public class UseOfUndeclaredType : SingleLocationError {

		public UseOfUndeclaredType(string file, int line, string name) : base(file, line) {
			this.name = name;
		}


		private string name;

		public override string GetMessage() => $"UseOfUndeclaredType ### {GetLocationString}, Name: {name}";

	}

}