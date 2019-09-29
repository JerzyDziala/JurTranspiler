namespace JurTranspiler.syntax_tree.Interfaces {

    public interface IVariableDeclarationSyntax : ISyntaxNode {

        string Name { get; }

        //is null when inferred variable
        ITypeSyntax? Type { get; }
    }

}