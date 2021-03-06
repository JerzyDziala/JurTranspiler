using JurTranspiler;
using JurTranspiler.Analysis.errors;
using JurTranspiler.Analysis.errors.bases;
using NUnit.Framework;

namespace JurTests {

	[TestFixture]
	public class FunctionCallsTests {

		[Test]
		[Parallelizable]
		public void SimpleFunctionCall() {
			var code = @"
        		abstraction 0 {
        		    num add(num a, num b){
                        result := a + b;
                        return result;
                    }
        		}
        		main {
                    a := add(5, 3+4);
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void SimpleGenericCall() {
			var code = @"
        		abstraction 0 {
        		    T Get<T>(T a){
						return a;
					}
        		}
        		main {
					mutable a := Get(5);
					a = ""jur"";
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new TypeMismatchInAssignmentError("__TEST__",
				                                  9,
				                                  "num",
				                                  "string"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void SimpleGenericCallWithExplicitTypeArgument() {
			var code = @"
        		abstraction 0 {
        		    T Get<T>(){
						return T.default;
					}
        		}
        		main {
					string g = Get<string>();
					num n = Get<string>();
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new TypeMismatchInAssignmentError("__TEST__",
				                                  9,
				                                  "num",
				                                  "string")
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void GenericFunctionPointerParameterContravariantce() {
			var code = @"
        		abstraction 0 {
        		    T A<T>(void(T) fun) {
                        T x;
                        return x;
					}
        		}
        		main {
                    void(string[]) f;
					string[] x = A(f);
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void NoTypeArgumentSpecified() {
			var code = @"
        		abstraction 0 {
        		    void generic<T>(string s){}
        		}
        		main {
        		    generic(""string"");
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new NoMatchingOverloadForCall("__TEST__", 6, "generic(string)"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void ExplicitTypeArguments() {
			var code = @"
        		abstraction 0 {
                    bool gen<G,T>(G g, string b){
                        return b == ""aqq"";
                    }
                    T gen<R,T>(R g, T b){
                        return b;
                    }
                    R gen<R,T>(string g, T b){
                        R r;
                        return r;
                    }
                    R gen<R>(R g, num b){
                        return g;
                    }
        		}
        		main {
        		    bool b = gen<bool,string>(true,""aqq"");
        		    num n = gen<string,num>(5,5);
        		    num n1 = gen(7,5);
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new CouldNotResolveAmbiguousFunctionCall("__TEST__", 18, "gen<bool,string>(bool,string)"),
				new CouldNotResolveAmbiguousFunctionCall("__TEST__", 20, "gen(num,num)"),
				new NoMatchingOverloadForCall("__TEST__", 19, "gen<string,num>(num,num)"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void SimpleDuplicatesAndAmbiguityCall() {
			var code = @"
        		abstraction 0 {
					T gen<T>(T a) {
                        return a;
					}

                    G gen<G>(G b) {
                        return b;
                    }
                    num gen(num a){
                        return a;
                    }
                    num gen(num a){
                        return a + 5;
                    }
        		}
        		main {
                    num x = gen(""str"");
                    num y = gen(5);
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new MultipleDeclarationsOfFunction(new Location[] {
					                                   new Location("__TEST__", 3),
					                                   new Location("__TEST__", 7),
				                                   },
				                                   "gen"),
				new MultipleDeclarationsOfFunction(new Location[] {
					                                   new Location("__TEST__", 10),
					                                   new Location("__TEST__", 13),
				                                   },
				                                   "gen"),
				new CouldNotResolveAmbiguousFunctionCall("__TEST__", 18, "gen(string)"),
				new CouldNotResolveAmbiguousFunctionCall("__TEST__", 19, "gen(num)"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void SimpleConstraints() {
			var code = @"
        		abstraction 0 {
        		    void fun<T,T1>(T a, T1 b) where T1 is T {

                    }
                    struct Entity {
                        num id;
                    }
                    struct Person {
                        is Entity;
                        string name;
                    }
        		}
        		main {
        		    fun(new Entity, new Person);
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void SimpleResolution() {
			var code = @"
        		abstraction 0 {

                    bool Fun(string a, num b){
                        return true;
                    }
                    num Fun(bool[] a, string b){
                        return 2 + 2;
                    }

                    Dog[] Fun(Dog a, Dog e){
                        return new Dog[];
                    }
                    struct Dog {
                        string name;
                    }
                    struct BigDog {
                        is Dog;
                        num size;
                    }
        		}
        		main {

                    Dog(string,string) Fun;

        		    bool b = Fun(""aqq"",5);
                    Dog d = Fun(""kluska"",""kluska"");
                    num n = Fun(new bool[], ""array"");

                    Dog[] dogs = Fun(new Dog,new BigDog);
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void SubstitutionsAndInheritance() {
			var code = @"
        		abstraction 0 {
        		    void add<T>(T[] list, T item){

                    }
                    struct Dog {
                        is Entity<num>;
                        string name;
                    }
                    struct Entity<T> {
                        T id;
                    }
        		}
        		main {
                    new Entity<num>[].add(new Entity<num>);
                    new Entity<num>[].add(new Dog);
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new NoMatchingOverloadForCall("__TEST__", 16, "add(Entity<num>[],Dog)"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void ValidSubstitutionsAndInheritance() {
			var code = @"
        		abstraction 0 {
        		    void add<T,I>(T[] list, I item) where I is T {
                        T a = item;
                    }
                    struct Dog {
                        is Entity<num>;
                        string name;
                    }
                    struct Entity<T> {
                        T id;
                    }
        		}
        		main {
                    new Entity<num>[].add(new Entity<num>);
                    new Entity<num>[].add(new Dog);
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void LambdaReturningUndeclaredStructTypeAsFunctionArgumentCompilerCrash() {
			var code = @"
        		abstraction 0 {
                    num fun(num(num) arg ){
                        return arg(5);
                    }
        		}
        		main {
                    fun( (num x) -> new Undeclared );
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new UseOfUndeclaredType("__TEST__", 8, "Undeclared"),
				new NoMatchingOverloadForCall("__TEST__", 8, "fun(Undeclared(num))")
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void MultilineLambdaReturningUndeclaredStructTypeAsFunctionArgument() {
			var code = @"
        		abstraction 0 {
                    num fun(num(num) arg ){
                        return arg(5);
                    }
        		}
        		main {
                    fun( (num x) -> {
                        return new Undeclared;
                    });
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new UseOfUndeclaredType("__TEST__", 9, "Undeclared"),
				new NoMatchingOverloadForCall("__TEST__", 8, "fun(Undeclared(num))")
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void IgnoreConstraintViolationIfArgIsUndefined() {
			var code = @"
        		abstraction 0 {
					T g<T, T2>(T[] arg1, T2 arg2) where T2 is T {
						return T.default;
					}
					struct A {
						num a;
					}
        		}
        		main {
					undef := 1 * ""aqq"";
					A x = g(new A[], undef );
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new TypeMismatchInUseOfOperator("__TEST__",
				                                11,
				                                "*",
				                                "num",
				                                "string"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}
		
		
		[Test]
		[Parallelizable]
		public void TrivialGenericNominallyTypedFunctionParameterSubstitution() {
			var code = @"
        		abstraction 0 {

					nominal struct A<T> { 
						T genericField
					}

					struct B {
						is A<string>
						string a	
					}

					struct C {
						string genericField
						string a	
					}

					struct D {
						is A<B>
						string a	
					}
				
					T f<T>(A<T> arg) -> T.default
					
        		}
        		main {
					string s = f(new B)
					string s1 = f(new C)
					B b = f(new D)
				}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { new NoMatchingOverloadForCall("__TEST__", 28, "f(C)") };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}

		[Test]
		[Parallelizable]
		public void GenericSubstitutionWithNominalTypesInContravariantSituation() {
			var code = @"
        		abstraction 0 {

					nominal struct A<T> { 
						T genericField
					}

					struct B {
						is A<string>
						string a	
					}

					struct C {
						string genericField
						string a	
					}

				
					T f<T>(void(B, T) arg) -> T.default
					
        		}
        		main {
					void(A<string>, B) validArg1
					void(B, B) validArg2
					void(any, B) validArg3
					void(A<any>, B) validArg4
					void(C, C) validArg5

					A<any> s1 = f(validArg1)
					A<string> s2 = f(validArg2)
					B s3 = f(validArg3)
					any s4 = f(validArg4)
					B s5 = f(validArg5)
				}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}
	}

}