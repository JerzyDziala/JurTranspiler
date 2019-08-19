namespace JurTranspiler.compilerSource.Analysis {

	public class UseOfUndeclaredVariable : SingleLocationError {

		private string name;


		public UseOfUndeclaredVariable(string file, int line, string name) : base(file, line) {
			this.name = name;
		}


		protected override string MessageBody => $"Name: {name}";

	}

}