using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

	public class StructDeclarationContainsInheritanceCycles : SingleLocationError {

		private string structName { get; }


		public StructDeclarationContainsInheritanceCycles(string file, int line, string structName) : base(file, line) {
			this.structName = structName;
		}


		protected override string MessageBody => $"StructName: {structName}";

	}

}