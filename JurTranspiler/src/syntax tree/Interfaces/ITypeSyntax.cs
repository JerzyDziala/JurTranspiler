namespace JurTranspiler.syntax_tree.Interfaces {

    public interface ITypeSyntax : ISyntaxNode {
        string Name { get; }
        string FullName { get; }
    }

}