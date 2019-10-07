using JurTranspiler;
using JurTranspiler.Analysis.errors.bases;
using NUnit.Framework;

namespace JurTests {

    [TestFixture]
    public class ForLoopTests {
        [Test]
        [Parallelizable]
        public void WhileLoop() {
            var code = @"
        		main {

                    mutable i := 0;

                    for i < 10 {
                        x := i * i;
                        i = i + 1;
                    }
        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] { };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void ParentlessWhileLoop() {
            var code = @"
        		main {

                    mutable i := 0;

                    for i < 10
                        i = i + 1;

        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] { };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void InitializedWhileLoop() {
            var code = @"
        		main {
                    for mutable num i = 0; i < 10 {
                        x := i * i;
                        i = i + 1;
                    }
        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] { };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void InferredInitializedWhileLoop() {
            var code = @"
        		main {
                    for mutable i := 0; i < 10 {
                        x := i * i;
                        i = i + 1;
                    }
        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] { };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void ForWithNoParents() {
            var code = @"
        		main {
                    for mutable i := 0; i < 10
                        i = i + 1;
        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] { };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void NestedFor() {
            var code = @"
        		main {
                    for mutable i := 0; i < 10 {
                        for mutable y := 0; y < 10 {
                            y = y + 1;
                        }
                        i = i + 1;
                    }
        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] { };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }
    }

}