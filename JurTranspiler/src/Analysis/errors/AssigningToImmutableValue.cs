using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {
    public class AssigningToImmutableValue : SingleLocationError{
        
        public AssigningToImmutableValue(string file, int line) : base(file, line) {
        }

        public AssigningToImmutableValue(Location location) : base(location) {
        }

        protected override string MessageBody => "cannot assign to an immutable value";
    }
}