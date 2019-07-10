namespace JurTranspiler.compilerSource.Analysis {

    public class StructDeclarationContainsInheritanceCycles : SingleLocationError{

        private string StructName;


        public StructDeclarationContainsInheritanceCycles(string file, int line, string structName) : base(file, line) {
            StructName = structName;
        }


        public override string GetMessage() => $"StructDeclarationContainsInheritanceCycles ### {GetLocationString} ### StructName: {StructName}";
    }

}