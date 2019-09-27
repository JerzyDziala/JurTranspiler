namespace JurTranspiler.compilerSource.nodes {

    public interface IExpressionSyntax : ISyntaxNode{

        bool CanBeAssignedTo { get; }

    }

}