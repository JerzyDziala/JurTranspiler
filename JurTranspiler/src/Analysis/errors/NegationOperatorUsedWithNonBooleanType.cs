using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

    public class NegationOperatorUsedWithNonBooleanType : SingleLocationError{

        private string typeName { get; }

        public NegationOperatorUsedWithNonBooleanType (string file,
                                           int line,
                                           string typeName) : base(file, line) {
            this.typeName = typeName;
        }


        protected override string MessageBody => $"cannot perform operation '!' on value of type '{typeName}'";
    }

}