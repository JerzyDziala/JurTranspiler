using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource;
using JurTranspiler.compilerSource.Analysis;
using NUnit.Framework;

namespace JurTranspilerTests {

	[TestFixture]
	public class InitializersTests {

		[Test]
		[Parallelizable]
		public void InitializerTest() {
			var code = @"
        		abstraction 0 {
        		    struct A<T> {
						T a;
						A<T> b;
						num c;
						A<num> d;
					}
        		}
        		main {
                    x := new A<string> {
						a = 'aqq',
						b = new A<string> { a = 'asd', b = new A<string> { a = 'aqq' }},
						c = 5,
						d = new A<num> { a = 5 }
					};
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void MissingFieldInitializer() {
			var code = @"
        		abstraction 0 {
        		    struct A<T> {
						T a;
						A<T> b;
						num c;
						A<num> d;
					}
        		}
        		main {
					x := new A<string> { x = 'aqq' };
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new NoMatchingFieldFound("__TEST__",
				                         11,
				                         "x",
				                         "A<string>"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void TypeMismatchInFieldInitializer() {
			var code = @"
        		abstraction 0 {
        		    struct A<T> {
						T a;
						A<T> b;
						num c;
						A<num> d;
					}
        		}
        		main {
					x := new A<string> { a = 5 };
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new TypeMismatchInAssignmentError("__TEST__",
				                                  11,
				                                  "string",
				                                  "num"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void InvalidUseOfConstructor() {
			var code = @"
        		abstraction 0 {

        		}
        		main {
					x := new num;
					y := new void(num) { x = 5 };
					z := new bool { z = true };
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new InvalidUseOfNew("__TEST__", 6, "num"),
				new InvalidUseOfNew("__TEST__", 7, "void(num)"),
				new InvalidUseOfNew("__TEST__", 8, "bool"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}

	}

}