namespace JurTranspiler.compilerSource.Analysis {

    public class CallLocation {
        public string File { get; }
        public int Line { get; }
        public string CallString{get;}


        public CallLocation(string file, int line, string callString) {
            File = file;
            Line = line;
            CallString = callString;
        }

    }

}