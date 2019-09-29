namespace JurTranspiler.syntax_tree.Interfaces {

    public interface IExpressionSyntax : ISyntaxNode{

        bool CanBeAssignedTo { get; }

    }

}