using JurTranspiler;
using JurTranspiler.Analysis.errors;
using JurTranspiler.Analysis.errors.bases;
using NUnit.Framework;

namespace JurTests {

	[TestFixture]
	public class AssignmentsAndConversionsTests {

		[Test]
		[Parallelizable]
		public void AssigningUndeclaredTypeToUndeclaredTypeError() {
			var code = @"
                        main {
                            A a = new B;
                        }
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new UseOfUndeclaredType(file: "__TEST__", line: 3, name: "A"),
				new UseOfUndeclaredType(file: "__TEST__", line: 3, name: "B"),
				new TypeMismatchInAssignmentError(file: "__TEST__",
				                                  line: 3,
				                                  leftName: "A",
				                                  rightName: "B"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		public void AssigningNumToStringAndBoolToStringError() {
			var code = @"
                        main {
                            mutable string a = 5;
                            a = true;
                        }
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new TypeMismatchInAssignmentError("__TEST__",
				                                  3,
				                                  "string",
				                                  "num"),
				new TypeMismatchInAssignmentError("__TEST__",
				                                  4,
				                                  "string",
				                                  "bool"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		public void AssigningUndeclaredToUndeclaredNoErrors() {
			var code = @"
                        main {
                           A a =
                           new A;
                           A[] b =
                           new A[];
                        }
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new UseOfUndeclaredType(file: "__TEST__", line: 3, name: "A"),
				new UseOfUndeclaredType(file: "__TEST__", line: 4, name: "A"),
				new UseOfUndeclaredType(file: "__TEST__", line: 5, name: "A"),
				new UseOfUndeclaredType(file: "__TEST__", line: 6, name: "A"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void SomeValidAssignmentsNoErrors() {
			var code = @"
        		abstraction 0 {
        		    struct Entity<T> {
                        T id;
                    }
                    struct NamedEntity<T>{
                        is Entity<T>;
                        string name;
                    }
                    struct Person {
                        is NamedEntity<string>;
                        num age;
                    }
                    struct Any {

                    }
        		}
        		main {
        		    Entity<string> entity = new Person;
                    NamedEntity<string> namedEntity = new Person;
                    Any an = namedEntity;
                    Entity<Entity<string>> ees = new NamedEntity<Entity<string>>;
                    Any[] a = new Person[];

                    NamedEntity<num>(Entity<string>, Entity<Person>) f1;
                    Entity<num>(Person, NamedEntity<Person>) f2 = f1;
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void SomeInvalidAssignments() {
			var code = @"
        		abstraction 0 {
        		    struct Entity<T> {
                        T id;
                    }
                    struct NamedEntity<T>{
                        is Entity<T>;
                        string name;
                    }
                    struct Person {
                        is NamedEntity<string>;
                        num age;
                    }
                    struct Any { }
        		}
        		main {
        		    Entity<Entity<string>> e = new Entity<Person>;
        		    Entity<Any> r = new Person;
                    Person p = new NamedEntity<string>;
                    Person[] b = new Any[];

                    Entity<num>(Person, NamedEntity<Person>) f2;
                    NamedEntity<num>(Entity<string>, Entity<Person>) f1 = f2;

                    void(Person) f3;
                    void(Entity<string>) f = f3;
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new TypeMismatchInAssignmentError(file: "__TEST__",
				                                  line: 18,
				                                  leftName: "Entity<Any>",
				                                  rightName: "Person"),
				new TypeMismatchInAssignmentError(file: "__TEST__",
				                                  line: 19,
				                                  leftName: "Person",
				                                  rightName: "NamedEntity<string>"),
				new TypeMismatchInAssignmentError(file: "__TEST__",
				                                  line: 20,
				                                  leftName: "Person[]",
				                                  rightName: "Any[]"),
				new TypeMismatchInAssignmentError(file: "__TEST__",
				                                  line: 23,
				                                  leftName: "NamedEntity<num>(Entity<string>,Entity<Person>)",
				                                  rightName: "Entity<num>(Person,NamedEntity<Person>)"),
				new TypeMismatchInAssignmentError(file: "__TEST__",
				                                  line: 26,
				                                  leftName: "void(Entity<string>)",
				                                  rightName: "void(Person)"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void SimpleFieldAssignment() {
			var code = @"
        		abstraction 0 {
        		    struct A {
                        mutable string a;
                    }
        		}
        		main {
        		    A x = new A;
                    x.a = ""jur"";
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void FieldAccessError() {
			var code = @"
        		abstraction 0 {
        		    struct A {
                    }
        		}
        		main {
        		    A x = new A;
                    x.a = ""jur"";
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new NoMatchingFieldFound(file: "__TEST__",
				                         line: 8,
				                         fieldName: "a",
				                         typeName: "A")
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void AmbiguousFieldAccess() {
			var code = @"
        		abstraction 0 {
        		    struct A {
                        mutable string a;
                        mutable A a;
                    }
        		}
        		main {
        		    A x = new A;
                    x.a = ""jur"";
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new AmbiguousFieldReference(file: "__TEST__",
				                            line: 10,
				                            fieldName: "a",
				                            typeName: "A"),
				new MultipleFieldsWithTheSameName(new Location[] { new Location("__TEST__", 4), new Location("__TEST__", 5) },
				                                  "a"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void FieldAccessJoinedField() {
			var code = @"
        		abstraction 0 {
        		    struct A {
                        mutable A a;
                        mutable A a;
                        is B;
                    }
                    struct B {
                        mutable A a;
                        is C<A>;
                    }
                    struct C<T>{
                        mutable T a;
                    }
        		}
        		main {
        		    A x = new A;
                    x.a = x;
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void FieldAccessGeneric() {
			var code = @"
        		abstraction 0 {
        		    struct A<T,G> {
                        mutable T g;
                        mutable G g2;
                    }
        		}
        		main {
        		    A<string,bool> a = new A<string,bool>;
                    a.g = ""jur"";
                    a.g2 = true;
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void FieldAccessGenericError() {
			var code = @"
        		abstraction 0 {
        		    struct A<T,G> {
                        mutable T g;
                        mutable G g2;
                    }
        		}
        		main {
        		    A<string,bool> a = new A<string,bool>;
                    a.g = ""aqq"";
                    a.g2 = new A<void(num),string[]>;
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new TypeMismatchInAssignmentError(file: "__TEST__",
				                                  line: 11,
				                                  leftName: "bool",
				                                  rightName: "A<void(num),string[]>"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void FieldAccessGenericInvalidDeclaration() {
			var code = @"
        		abstraction 0 {
        		    struct A<T,T> {
                        T g;
                        G g2;
                    }
        		}
        		main {
        		    A<string,bool> a = new A<string,bool>;
                    a.g = true;
                    a.g2 = new A<void(num),string[]>;
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new MultipleTypeParametersWithTheSameName(locations: new Location[] { new Location("__TEST__", 3), new Location("__TEST__", 3) },
				                                          name: "T"),
				new UseOfUndeclaredType(file: "__TEST__", line: 5, name: "G"),

				//new UseOfUndeclaredType(file: "__TEST__", line: 9, name: "A<string,bool>"), //i don""t want to write a column number in test
				new UseOfUndeclaredType(file: "__TEST__", line: 9, name: "A<string,bool>"),
				new UseOfUndeclaredType(file: "__TEST__", line: 11, name: "A<void(num),string[]>"),
				new TriedToAccessFieldOnNonStruct(file: "__TEST__",
				                                  line: 10,
				                                  fieldName: "g",
				                                  typeName: "A<string,bool>"),
				new TriedToAccessFieldOnNonStruct(file: "__TEST__",
				                                  line: 11,
				                                  fieldName: "g2",
				                                  typeName: "A<string,bool>"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void ComplexGenericBugRegressionTest() {
			var code = @"
                main {
                    x := new A<bool>;
                    x.a = new B<A<string>>;
                }

                abstraction 0 {

                    struct A<T1> {
                        mutable B<A<string>> a;
                    }

                    struct B<T2> {

                    }

                }";

			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void AnotherComplexGenericBugRegressionTest() {
			var code = @"
                main {

                }

                abstraction 0 {

                    struct A<T> {
                        T x;
                    }

                    A<T> create<T>(){
                        return new A<T>;
                    }

                    A<T> _create<T>() {
                        return create<T>();
                    }
                }";

			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void ComplexCodeGenerationGenericsBug() {
			var code = @"
                main {
                    fun<num>();
                }

                abstraction 2 {

                    void fun<T>() {

                        x := new Dog<T>;

                        x.d = new Cat<T, Dog<string>>;

                    }

                    struct Dog<T> {
                        mutable Cat<T, Dog<string>> d;
                    }

                    struct Cat<T,R> {
                        T field1;
                    }
                }";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}

	}

}