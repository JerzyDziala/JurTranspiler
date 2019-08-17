using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;

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

//---User's code---//
{syntaxTree.ToJs(knowledge)}
";
        }

    }

}