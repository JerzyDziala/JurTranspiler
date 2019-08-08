namespace JurTranspiler.compilerSource.Analysis {

    public class TriedToAccessFieldOnNonStruct : SingleLocationError {

        private string fieldName;
        private string typeName;


        public TriedToAccessFieldOnNonStruct(string file,
                                             int line,
                                             string fieldName,
                                             string typeName) : base(file, line) {
            this.fieldName = fieldName;
            this.typeName = typeName;
        }


        public override string GetMessage() => $"TriedToAccessFieldOnNonStruct ### {GetLocationString}, FieldName: {fieldName}, on type: {typeName}";
    }

}