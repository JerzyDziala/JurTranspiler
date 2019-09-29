using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

    public class TriedToAccessFieldOnNonStruct : SingleLocationError {

        private readonly string fieldName;
        private readonly string typeName;


        public TriedToAccessFieldOnNonStruct(string file,
                                             int line,
                                             string fieldName,
                                             string typeName) : base(file, line) {
            this.fieldName = fieldName;
            this.typeName = typeName;
        }


        protected override string MessageBody => $"FieldName: {fieldName}, on type: {typeName}";

    }

}