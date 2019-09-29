using JurTranspiler.Analysis.errors.bases;

namespace JurTranspiler.syntax_tree.Interfaces {

    public interface IHaveLocation {
        Location Location { get; }
    }

}