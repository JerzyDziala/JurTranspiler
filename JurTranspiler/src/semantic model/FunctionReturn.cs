using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.compilerSource.semantic_model {

    public class FunctionReturn : IHaveLocation, IHaveType {
        public Location Location { get; }
        public IType Type { get; }


        public FunctionReturn(Location location, IType type) {
            Location = location;
            Type = type;
        }
    }

}