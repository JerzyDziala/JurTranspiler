using System.Collections.Generic;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.Analysis {

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