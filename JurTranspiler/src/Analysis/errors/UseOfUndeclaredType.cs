namespace JurTranspiler.compilerSource.Analysis {

	public class UseOfUndeclaredType : SingleLocationError {

		public UseOfUndeclaredType(string file, int line, string name) : base(file, line) {
			this.name = name;
		}


		private string name;

		protected override string MessageBody => $"Name: {name}";

	}

}