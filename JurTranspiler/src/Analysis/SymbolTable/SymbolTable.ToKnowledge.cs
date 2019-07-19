namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable {

        public Knowledge ToKnowledge() {
            return new Knowledge(TypesBindings.Values,
                                 TypesBindings,
                                 FieldsBindings,
                                 NewNames,
                                 FunctionSignaturesBindings,
                                 FunctionCallsBindings,
                                 OpenStructsBinding);
        }

    }

}