namespace JurTranspiler.compilerSource.semantic_model.functions {

    public interface ICallable {
        string Name { get; }
        int Arity { get; }
        Type ReturnType { get; }
    }

}