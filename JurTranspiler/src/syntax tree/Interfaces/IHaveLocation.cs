using JurTranspiler.compilerSource.Analysis;

namespace JurTranspiler.syntax_tree.Interfaces {

    public interface IHaveLocation {
        Location Location { get; }
    }

}