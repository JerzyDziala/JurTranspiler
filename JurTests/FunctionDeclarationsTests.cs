using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource;
using JurTranspiler.compilerSource.Analysis;
using NUnit.Framework;

namespace JurTranspilerTests {

    [TestFixture]
    public class FunctionDeclarationsTests {

        [Test]
        [Parallelizable]
        public void CorrectFunctionDeclarations() {
            var code = @"
        		abstraction 0 {
        	        void Func(string s){
                        a := 5;
                    }

                    string Func1(){
                        return 'jur';
                    }

                    void(num) Func2(){
                        void(num) f;
                        return f;
                    }

                    T Func3<T>(T x){
                        return x;
                    }

                    T Func4<T,T1>(T x, T x1, T1 y){
                        a := x;
                        b := x1;
                        c := y;
                        return b;
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
        public void FunctionDuplicateParametersError() {
            var code = @"
        		abstraction 0 {
        		    void Func(string a, string a){
                        num a = 5;
                    }
        		}
        		main {

        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] {
                new MultipleDeclarationsOfVariableInScope(locations: new Location[] {
                                                              new Location("__TEST__", 3),
                                                              new Location("__TEST__", 3),
                                                              new Location("__TEST__", 4)
                                                          },
                                                          name: "a"),
            };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void FunctionDuplicateTypeParametersError() {
            var code = @"
        		abstraction 0 {
        		    void Func<T,T,T1>(num x){

                    }
        		}
        		main {

        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] {
                new MultipleTypeParametersWithTheSameName(locations: new Location[] {
                                                              new Location("__TEST__", 3),
                                                              new Location("__TEST__", 3)
                                                          },
                                                          name: "T")
            };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void CorrectUseOfTypeParametersInFunction() {
            var code = @"
        		abstraction 0 {
        	        T func<T>(T t){
                        T a = t;
                        g := a;
                        T[][] ar;
                        T[][] ar2 = ar;
                        return g;
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
        public void DeclarationWithExplicitGenericTypeParameter() {
            var code = @"
        		abstraction 0 {
                    T[] createArrayOf<T>(num count){
                        T t;
                        T g = t;
                        c := count;
                        c = 5;
                        return new T[];
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
        public void SimpleConstraintNoError() {
            var code = @"
        		abstraction 0 {
                    void F<T,G>(T vague, G specific) where G is T{
                        vague = specific;
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
        public void UndeclaredTypeInConstraint() {
            var code = @"
        		abstraction 0 {
                    void F<T,G>(T vague, G specific) where G is H
                                                       and G is T
                                                       and V is List<T> {
                        vague = specific;
                    }
                    struct List<B>{
                        B[] data;
                    }
        		}
        		main {

        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] {
                new UseOfUndeclaredType("__TEST__", 3, "H"),
                new UseOfNonTypeParameterInConstraint("__TEST__", 3, "H"),
                new UseOfUndeclaredType("__TEST__", 5, "V"),
                new UseOfNonTypeParameterInConstraint("__TEST__", 5, "V"),
                new UseOfNonTypeParameterInConstraint("__TEST__", 5, "List<T>"),
            };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }


        [Test]
        [Parallelizable]
        public void MultipleConstraintsOnOneTypeParameter() {
            var code = @"
        		abstraction 0 {
        		    void F<T,T1,T2>(T[] arr, T1 item1, T2 item2) where T is T1
                                                                    and T is T2
                    {

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
        public void CycleInConstraint() {
            var code = @"
        		abstraction 0 {
        		    void F<T>() where T is T
                                  and T is T { }
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
        public void ComplexConstraintNoError() {
            var code = @"
        		abstraction 0 {
        		    T Fun<T,T1,T2,T3,T4>(T[][][] a, T1 b, T2 c) where T is T2
                                                                and T is T3
                                                                and T3 is T
                                                                and T3 is T2 {
                        T3 x;
                        return x;
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
        public void ValidAssignmentsOfTypeParameters() {
            var code = @"
        		abstraction 0 {
                    T Func<T>(T a, T b) {
                        a = b;
                        b = a;
                        return a;
                    }
                    T Func<T,T1>(T a, T1 b) where T1 is T {
                        a = b;
                        return b;
                    }
                    T Func<T,T1,T2>(T a, T1 b, T2 c) where T2 is T1
                                                       and T1 is T {
                        a = c;
                        b = c;
                        a = a;
                        b = b;
                        c = c;
                        return c;
                    }
        		}
        		main {

        		}
        ";
            var (errors, _) = Compiler.Compile(code);
            var expectedErrors = new Error[] { };
            CollectionAssert.AreEquivalent(expectedErrors, errors);
        }

    }

}