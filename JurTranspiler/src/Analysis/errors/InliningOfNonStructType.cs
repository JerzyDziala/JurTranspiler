using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.compilerSource.nodes {

    public class InliningOfNonStructType : SingleLocationError {

        private string name;


        public InliningOfNonStructType(string file, int line, string name) : base(file, line) {
            this.name = name;
        }


        protected override string MessageBody => $"Name: {name}";

    }

}