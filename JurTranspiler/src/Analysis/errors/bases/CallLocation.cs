namespace JurTranspiler.compilerSource.Analysis {

    public class CallLocation : Location {
        public string CallString { get; }


        public CallLocation(string file, int line, string callString) : base(file, line) {
            CallString = callString;
        }

    }

}