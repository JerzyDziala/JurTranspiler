using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

    public class AmbiguousFieldReference : SingleLocationError{

        private string fieldName;
        private string typeName;

        public AmbiguousFieldReference(string file, int line, string fieldName,string typeName) : base(file, line) {
            this.fieldName = fieldName;
            this.typeName = typeName;
        }





        public override string GetMessage() => $"AmbiguousFieldReference ### {GetLocationString}, FieldName: {fieldName}, on type: {typeName}";
    }

}