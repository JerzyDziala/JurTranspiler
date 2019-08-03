using System.Linq;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable {

        public Knowledge ToKnowledge() {
            return new Knowledge(TypesBindings.Values.Concat(OpenStructsBinding.Values),
                                 TypesBindings,
                                 FieldsBindings,
                                 NewNames,
                                 FunctionSignaturesBindings,
                                 FunctionCallsBindings,
                                 OpenStructsBinding,
                                 ExpressionsBindings);
        }

    }

}