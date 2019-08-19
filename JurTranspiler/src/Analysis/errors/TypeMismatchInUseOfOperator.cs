using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

    public class TypeMismatchInUseOfOperator : SingleLocationError {

        public TypeMismatchInUseOfOperator(string file,
                                           int line,
                                           string op,
                                           string leftName,
                                           string rightName) : base(file, line) {
            this.op = op;
            this.leftName = leftName;
            this.rightName = rightName;
        }


        private string op;
        private string leftName;
        private string rightName;

        protected override string MessageBody => $"cannot perform operation: '{op}' on values of types '{leftName}' and '{rightName}'";

    }

}