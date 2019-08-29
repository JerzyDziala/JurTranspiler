using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.Analysis.errors {

    public class NotAllCodePathReturnValue : SingleLocationError {

        public NotAllCodePathReturnValue(string file, int line) : base(file, line) {
        }


        public NotAllCodePathReturnValue(Location location) : this(location.File, location.Line) {
        }


        public NotAllCodePathReturnValue(IHaveLocation thing) : this(thing.Location) {
        }


        protected override string MessageBody => "";
    }

}