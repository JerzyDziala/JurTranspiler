using JurTranspiler.compilerSource;
using JurTranspiler.compilerSource.Analysis;
using NUnit.Framework;

namespace JurTranspilerTests {

    [TestFixture]
    public class VariablesScopesTest {

        [Test]
        [Parallelizable]
        public void SimpleDuplicate() {
            var code = @"
        		abstraction 0 {
        		    void x() {
                        num a = 5;
                        a := 4;
                    }
        		}
        		main {

        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] {
                new MultipleDeclarationsOfVariableInScope(new (string file, int line)[] {
                    ("__TEST__",4),
                    ("__TEST__",5)
                },"a" ),
            };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }

        [Test]
        [Parallelizable]
        public void FunctionParametersDuplicate() {
            var code = @"
        		abstraction 0 {
        		    void x(string a) {
                        num a = 5;
                        a := 4;
                    }
        		}
        		main {

        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] {
                new MultipleDeclarationsOfVariableInScope(new (string file, int line)[] {
                    ("__TEST__",3),
                    ("__TEST__",4),
                    ("__TEST__",5)
                },"a" ),
            };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void IfsAndForDuplicates() {
            var code = @"
        		abstraction 0 {
        		    void x(string a) {
                        num a;
                        if a == 0 {
                            num a;
                        }
                        else num a;
                        for a := 0; a < 0 {
                            num a;
                        }
                        num a;
                    }
                    num add(num a){
                        num a;
                        return a;
                    }
        		}
        		main {
                    num a;
        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] {
                new MultipleDeclarationsOfVariableInScope(new (string file, int line)[] {
                    ("__TEST__",3),
                    ("__TEST__",4),
                    ("__TEST__",6),
                    ("__TEST__",8),
                    ("__TEST__",9),
                    ("__TEST__",10),
                    ("__TEST__",12)
                },"a" ),
                new MultipleDeclarationsOfVariableInScope(new (string file, int line)[] {
                    ("__TEST__",14),
                    ("__TEST__",15)
                },"a" ),
            };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }

        [Test]
        [Parallelizable]
        public void UseInDeclaration() {
            var code = @"
        		abstraction 0 {

        		}
        		main {
                    num a = a + 1;
        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] {
                new UseOfUndeclaredVariable("__TEST__",6,"a"),
            };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


    }

}