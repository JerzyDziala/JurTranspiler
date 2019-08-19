namespace JurTranspiler.compilerSource.Analysis {

    public class TypeMismatchInAssignmentError :SingleLocationError{
        public TypeMismatchInAssignmentError(string file, int line, string leftName,
                                             string rightName) : base(file, line) {
            this.leftName = leftName;
            this.rightName = rightName;
        }


        private string leftName;
        private string rightName;

        protected override string MessageBody => $"cannot assign value of type {rightName} to an expression of type {leftName}";

    }

}