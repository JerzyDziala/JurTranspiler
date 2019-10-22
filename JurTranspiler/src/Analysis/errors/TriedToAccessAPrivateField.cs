using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

	public class TriedToAccessAPrivateField : SingleLocationError {

		public string Name { get; }
		public string Type { get; }


		public TriedToAccessAPrivateField(string file,
		                                  int line,
		                                  string name,
		                                  string type) : base(file, line) {
			Name = name;
			Type = type;
		}


		public TriedToAccessAPrivateField(Location location, string name, string type) : base(location) {
			Name = name;
			Type = type;
		}


		protected override string MessageBody => $"cannot access private field '{Name}' on type '{Type}'";

	}

}