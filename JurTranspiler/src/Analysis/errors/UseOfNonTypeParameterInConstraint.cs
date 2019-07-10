using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.Analysis.errors {

    public class UseOfNonTypeParameterInConstraint : SingleLocationError{

        public UseOfNonTypeParameterInConstraint(string file, int line, string name) : base(file, line) {
            this.name = name;
        }


        private string name;




        public override string GetMessage() => $" ###UseOfNonTypeParameterInConstraint {GetLocationString}, Name: {name}";
    }

}