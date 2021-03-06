using JurTranspiler.Analysis.errors.bases;
using JurTranspiler.semantic_model.types;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.semantic_model {

    public class FunctionReturn : IHaveLocation, IHaveType {
        public Location Location { get; }
        public IType Type { get; }


        public FunctionReturn(Location location, IType type) {
            Location = location;
            Type = type;
        }
    }

}