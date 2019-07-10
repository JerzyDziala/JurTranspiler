using System.CodeDom.Compiler;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.CodeGeneration {

	public class Generator {

		private Binder binder { get; }


		public Generator(Binder binder) {
			this.binder = binder;
		}


		public string generate(SyntaxTree tree) {
			var jsStructTypeConstructor = @"

function StructType$(name, genericParams, fields) {
    this.name = name;
    this.genericParams = genericParams;
    this.isGeneric = !genericParams.empty();
    this.fields = fields;
    this.fullName = name + (this.isGeneric ? ' < ' + this.genericParams.join(', ') + ' > ' : '');
}";

			var jsArrayTypeConstructor = @"

function ArrayType$(elementType) {
    this.elementType = elementType;
    this.fullName = elementType.fullName + '[]';
}";

			var jsPrimitiveTypeConstructor = @"

function PrimitiveType$(kind) {
    this.kind = kind;
    this.name = this.kind;
} ";

			var jsFieldConstructor = @"

function Field$(type, name) {
    this.type = type;
    this.name = name;
}
";

			var jsFunctionTypeConstructor = @"

function FunctionType$(){
    this.name = 'function'
}";


			return "";
		}


		private string generateJsTypesTable() {

			var structTypesDictionary = generateJsStructTypesTable();

			var table = $@"
	internals$ = {{
		structTypes$:

		arrayTypes$:

		functionType$: new FunctionType$(),

        primitiveTypes$: {{
            string: new PrimitiveType$('string'),
            bool: new PrimitiveType$('bool'),
            num: new PrimitiveType$('num'),
        }}
	}}
";
			return "";

		}


		private string generateJsStructTypesTable() {


			return "";
		}

	}

}