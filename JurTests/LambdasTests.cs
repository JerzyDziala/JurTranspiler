using JurTranspiler;
using JurTranspiler.Analysis.errors;
using JurTranspiler.Analysis.errors.bases;
using NUnit.Framework;

namespace JurTests {

	[TestFixture]
	public class LambdasTests {

		[Test]
		[Parallelizable]
		public void SimpleLambda() {
			var code = @"
       		main {
               string(num, num) asText = (num a, num b) -> a + "" "" + b;
       		}
       ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void SimpleLambdaIncorrectReturnType() {
			var code = @"
       		main {
               string(num, num) asText = (num a, num b) -> a + b;
       		}
       ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new TypeMismatchInAssignmentError("__TEST__",
				                                  3,
				                                  "string(num,num)",
				                                  "num(num,num)"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void SimpleLambdaIncorrectArguments() {
			var code = @"
       		main {
               string(num, num) asText = (string a, num b) -> a + b;
       		}
       ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new TypeMismatchInAssignmentError("__TEST__",
				                                  3,
				                                  "string(num,num)",
				                                  "string(string,num)"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void SimpleInference() {
			var code = @"
        		main {
        		    add := (num a, num b) -> a + b;
                    num c = add(5, 6);
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void UnableToInfer() {
			var code = @"
        		main {
        		    add := (num a, num b) -> {
                        if a > b return a;
                        else return ""error"";
                    };
                    inferred := add(5, 3);
                    num explicit = add(5, 3);
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new UnableToInferReturnType(new Location[] {
					new Location("__TEST__", 4),
					new Location("__TEST__", 5)
				})
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}

	}

}