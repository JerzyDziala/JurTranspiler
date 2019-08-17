namespace JurTranspiler.compilerSource.Analysis {

    public class SyntaxError : SingleLocationError{

        private string Details;


        public SyntaxError(string file, int line, string details) : base(file, line) {
            Details = details;
        }


        public override string GetMessage() => $"SyntaxError ### {GetLocationString} ### {Details}";
    }

}