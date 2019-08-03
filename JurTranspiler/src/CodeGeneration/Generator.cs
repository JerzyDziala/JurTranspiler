using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.CodeGeneration {

    public class Generator {

        private Knowledge knowledge { get; }
        private SyntaxTree syntaxTree { get; }


        public Generator(Knowledge knowledge, SyntaxTree syntaxTree) {
            this.knowledge = knowledge;
            this.syntaxTree = syntaxTree;
        }


        public string GenerateJs() {
            return $@"//Program//

//---Types Table---//

{new TypesTableGenerator(knowledge).GenerateTypesTable()}

//---internal javascript functions---//

{JsStrings.TypeOfDefinition}
{JsStrings.GetTypeDefinition}
{JsStrings.WithTypeNameDefinition}

//---User's code---//
{syntaxTree.ToJs(knowledge)}
";
        }

    }

}