using JurTranspiler;
using JurTranspiler.Analysis.errors;
using JurTranspiler.Analysis.errors.bases;
using NUnit.Framework;

namespace JurTests {

    [TestFixture]
    public class ReturnsTests {

        [Test]
        [Parallelizable]
        public void SimpleReturnStatement() {
            var code = @"
        		abstraction 0 {
        		    num add(num a, num b) {
                        return a + b;
                    }
        		}
        		main { }
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] { };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void SimpleArrowReturnStatement() {
            var code = @"
        		abstraction 0 {
        		    num add(num a, num b) -> a + b;
        		}
        		main { }
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] { };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void SimpleVoidReturnStatement() {
            var code = @"
        		abstraction 0 {
        		    void add(num a, num b) {
                        return;
                    }
        		}
        		main { }
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] { };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void ArrowReturnTypeMismatchVoid() {
            var code = @"
        		abstraction 0 {
        		    void add(num a, num b) -> a + b;
        		}
        		main { }
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] {
                new TypeMismatchInReturnStatement("__TEST__", 3, "num", "void"),
            };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void LambdaReturnType() {
            var code = @"
        		abstraction 0 {
        		    num add(num a, num b) {
                        lambda := () -> {
                            return a + b;
                        };
                    }
        		}
        		main { }
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] {
                new NotAllCodePathReturnValue("__TEST__", 3),
            };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void IfReturnType() {
            var code = @"
        		abstraction 0 {
        		    num add(num a, num b) {
                        if a > b {
                            return a + b;
                        }
                        else {
                            c := a + b;
                        }

                    }
        		}
        		main { }
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] {
                new NotAllCodePathReturnValue("__TEST__", 3),
            };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void ValidIfReturn() {
            var code = @"
        		abstraction 0 {
        		    num add(num a, num b) {
                        if a > b {
                            return a + b;
                        }
                        else if a == 5 {
                            return 5;
                        }
                        else a = 5;
                    }
        		}
        		main { }
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] {
                new NotAllCodePathReturnValue("__TEST__", 3),
            };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void SimpleReturnPaths() {
            var code = @"
        		abstraction 0 {
        		    num foo() {
                        if true return 1;
                    }
        		}
        		main { }
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] {
                new NotAllCodePathReturnValue("__TEST__", 3),
            };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void SimpleReturnPathsArrow() {
            var code = @"
        		abstraction 0 {
        		    num foo() -> 1;
        		}
        		main { }
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] { };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void SimpleReturnPathsIfElse() {
            var code = @"
        		abstraction 0 {
        		    num foo() {
                        if true return 1;
                        else return 2;
                    }
        		}
        		main {

        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] { };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void SimpleReturnPathsIfNoElse() {
            var code = @"
        		abstraction 0 {
        		    num foo() {
                        if true return 1;
                        return 2;
                    }
        		}
        		main {

        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] { };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void SimpleReturnPathsIfElseBlock() {
            var code = @"
        		abstraction 0 {
        		    num foo() {
                        if true return 1;
                        else {
                            return 2;
                        }
                    }
        		}
        		main {

        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] { };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void SimpleNoReturnPathsIfElseBlock() {
            var code = @"
        		abstraction 0 {
        		    num foo() {
                        if true return 1;
                        else {
                            x := 3;
                        }
                    }
        		}
        		main {

        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] {
                new NotAllCodePathReturnValue("__TEST__", 3),
            };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }
    }

}