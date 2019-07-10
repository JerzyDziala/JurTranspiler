namespace JurTranspiler.compilerSource.Analysis {

    public class TypeMismatchInAssignmentError :SingleLocationError{
        public TypeMismatchInAssignmentError(string file, int line, string leftName,
                                             string rightName) : base(file, line) {
            this.leftName = leftName;
            this.rightName = rightName;
        }


        private string leftName;
        private string rightName;





        public override string GetMessage() => $"TypeMismatchInAssignment ### {GetLocationString}, cannot assign value of type {rightName} to an expression of type {leftName}";
    }

}