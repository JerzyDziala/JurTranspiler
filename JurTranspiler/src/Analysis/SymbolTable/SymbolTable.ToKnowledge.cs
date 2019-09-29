using System.Collections.Generic;
using JurTranspiler.semantic_model.types;

namespace JurTranspiler.Analysis.SymbolTable {

    public partial class SymbolTable {

        public Knowledge ToKnowledge(IEnumerable<IType> allTypes) {

            return new Knowledge(allTypes,
                                 TypesBindings,
                                 FieldsBindings,
                                 NewFunctionNames,
                                 NewVariableNames,
                                 FunctionSignaturesBindings,
                                 FunctionCallsBindings,
                                 OpenStructsBinding,
                                 ExpressionsBindings);
        }

    }

}