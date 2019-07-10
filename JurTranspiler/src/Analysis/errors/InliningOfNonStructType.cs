using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.compilerSource.nodes {

    public class InliningOfNonStructType : SingleLocationError {

        private string name;


        public InliningOfNonStructType(string file, int line, string name) : base(file, line) {
            this.name = name;
        }


        public override string GetMessage() => $"InliningOfNonStructType ### {GetLocationString}, Name: {name}";
    }

}