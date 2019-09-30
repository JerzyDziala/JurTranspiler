using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.Analysis.errors {
    public class MemberFunctionWithoutArguments : SingleLocationError {

        private string Name { get; }
        
        public MemberFunctionWithoutArguments(string file, int line, string name) : base(file, line) {
            Name = name;
        }

        public MemberFunctionWithoutArguments(Location location,string name) : base(location) {
            Name = name;
        }

        protected override string MessageBody =>
            $"member function '{Name}' doesn't have any parameters. member functions have to have at least one.'";
    }
}