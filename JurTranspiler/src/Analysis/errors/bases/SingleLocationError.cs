using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.Analysis.errors.bases {

    public abstract class SingleLocationError : Error, IHaveLocation {

        protected override string Localization => $"file: {file}line: {line}";
        protected string file { get; }
        protected int line { get; }
        public Location Location => new Location(file, line);


        protected SingleLocationError(string file, int line) {
            this.file = file;
            this.line = line;
        }


        protected SingleLocationError(Location location) {
            this.file = location.File;
            this.line = location.Line;
        }



    }

}