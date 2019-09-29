using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {

    public class UseOfNonTypeParameterInConstraint : SingleLocationError{

        public UseOfNonTypeParameterInConstraint(string file, int line, string name) : base(file, line) {
            this.name = name;
        }


        private string name;

        protected override string MessageBody => $"Name: {name}";

    }

}