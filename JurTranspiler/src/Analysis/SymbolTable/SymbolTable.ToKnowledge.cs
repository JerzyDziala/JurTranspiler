using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;

namespace JurTranspiler.compilerSource.Analysis {

    public partial class SymbolTable {

        public Knowledge ToKnowledge(IEnumerable<Type> allTypes) {

            return new Knowledge(allTypes,
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