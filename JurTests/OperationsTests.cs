using JurTranspiler;
using JurTranspiler.Analysis.errors;
using JurTranspiler.Analysis.errors.bases;
using NUnit.Framework;

namespace JurTests {

	[TestFixture]
	public class OperationsTests {

		[Test]
		[Parallelizable]
		public void ValidOperations() {
			var code = @"
        		abstraction 0 {
        		    struct A {
                        num i;
                    }
                    struct B {
                        num i;
                    }
        		}
        		main {
        		    a := 0;
                    b := 0;
                    c := """";
                    d := """";
                    e := true;
                    f := false;
                    g := new A;
                    h := new B;

                    num i = a + b;
                    string i1 = a + c;
                    string i2 = c + d;
                    string i3 = f + c + e;
                    string i4 = d + a;
                    num i5 = a - b;
                    num i6 = a * b / a;
                    bool i7 = a > b + 5;
                    bool i8 = e || f && a / b < b;
                    bool i9 = g != h;
                    bool i10 = e != f;
                    bool i11 = h == h;
                    bool i13 = c != d;
                    bool i14 = i != a || a <= b && a >= b;

                    bool xxx = !true;

                    bool xxxx = !!true;
                    bool xxxxx = !!!false && !!(!!true || !!true);
                    bool xxxxxx = !!""aqq"" && !!(!!true || !!false);
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new NegationOperatorUsedWithNonBooleanType("__TEST__", 39, "string"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void InvalidNegation() {
			var code = @"
        		abstraction 0 {

        		}
        		main {
					num x = -5;
					num y = -""aqq"";
					num z = -(-(-x) - 4);
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new TypeMismatchInUseOfOperator("__TEST__",
				                                7,
				                                "-",
				                                "string"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}

	}

}