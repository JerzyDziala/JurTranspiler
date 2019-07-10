using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

    public class NoMatchingFieldFound : SingleLocationError{

        private string fieldName;
        private string typeName;

        public NoMatchingFieldFound(string file, int line, string fieldName,string typeName) : base(file, line) {
            this.fieldName = fieldName;
            this.typeName = typeName;
        }





        public override string GetMessage() => $"NoMatchingFieldFound ### {GetLocationString}, FieldName: {fieldName}, on type: {typeName}";    }

}