using JurTranspiler;
using JurTranspiler.Analysis.errors;
using JurTranspiler.Analysis.errors.bases;
using NUnit.Framework;

namespace JurTests {

	[TestFixture]
	public class StructDefinitionsTests {

		[Test]
		[Parallelizable]
		public void DirectCycle() {
			var code = @"
						abstraction 0 {
							struct A {
								is B;
							}
							struct B {
								is A;
							}
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new[] {
				new StructDeclarationContainsInheritanceCycles("__TEST__", 3, "A"),
				new StructDeclarationContainsInheritanceCycles("__TEST__", 6, "B")
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void IndirectCycle() {
			var code = @"
						abstraction 0 {
							struct A {
								is B;
							}
							struct B {
								is C;
							}
							struct C {
								is A;
							}
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new[] {
				new StructDeclarationContainsInheritanceCycles("__TEST__", 3, "A"),
				new StructDeclarationContainsInheritanceCycles("__TEST__", 6, "B"),
				new StructDeclarationContainsInheritanceCycles("__TEST__", 9, "C")
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void MultipleInheritanceNoError() {
			var code = @"
						abstraction 0 {
							struct A {
								is B;
                                is C;
							}
							struct B {
							}
							struct C {
                                is B;
							}
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[0];
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void ComplexMultipleInheritanceNoError() {
			var code = @"
						abstraction 0 {
							struct A {
								is B;
                                is C;
                                is D;
							}
							struct B {
                                is C;
                                is D;
                                is E;
							}
							struct C {
                                is E;
							}
                            struct D {
                                is C;
                            }
                            struct E {
                                string e;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[0];

			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void ComplexCycle() {
			var code = @"
						abstraction 0 {
							struct A {
								is B;
                                is C;
							}
							struct B {
                                is C;
							}
							struct C {
                                is A;
                                is D;
                                is E;
							}
                            struct D {
                                is E;
                            }
                            struct E {
                                is A;
                                is B;
                                string e;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new[] {
				new StructDeclarationContainsInheritanceCycles("__TEST__", 3, "A"),
				new StructDeclarationContainsInheritanceCycles("__TEST__", 7, "B"),
				new StructDeclarationContainsInheritanceCycles("__TEST__", 10, "C"),
				new StructDeclarationContainsInheritanceCycles("__TEST__", 15, "D"),
				new StructDeclarationContainsInheritanceCycles("__TEST__", 18, "E")
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void DuplicateFields() {
			var code = @"
						abstraction 0 {
                            struct A {
                                string a;
                                num a;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new[] {
				new MultipleFieldsWithTheSameName(locations: new Location[] {
					                                  new Location("__TEST__", 4),
					                                  new Location("__TEST__", 5)
				                                  },
				                                  name: "a")
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void DuplicateFieldsMoreThenTwo() {
			var code = @"
						abstraction 0 {
                            struct A {
                                string a;
                                num a;
                                bool a;
                                undeclared a;
                                string b;
                                bool[][][] b;
                                bool[][] b;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new MultipleFieldsWithTheSameName(locations: new Location[] {
					                                  new Location("__TEST__", 4),
					                                  new Location("__TEST__", 5),
					                                  new Location("__TEST__", 6),
					                                  new Location("__TEST__", 7)
				                                  },
				                                  name: "a"),
				new MultipleFieldsWithTheSameName(locations: new Location[] {
					                                  new Location("__TEST__", 8),
					                                  new Location("__TEST__", 9),
					                                  new Location("__TEST__", 10)
				                                  },
				                                  name: "b"),
				new UseOfUndeclaredType(file: "__TEST__", line: 7, name: "undeclared")
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void InheritanceCausesMultipleFieldsError() {
			var code = @"
						abstraction 0 {
                            struct A {
                                string a;
                                is B;
                            }
                            struct B {
                                bool a;
                                is C;
                            }
                            struct C {
                                num a;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new InliningResultsInMultipleFieldsWithTheSameName(file: "__TEST__",
				                                                   line: 3,
				                                                   fieldName: "a",
				                                                   fieldOwnersNames: new[] { "A", "B", "C" }),
				new InliningResultsInMultipleFieldsWithTheSameName(file: "__TEST__",
				                                                   line: 7,
				                                                   fieldName: "a",
				                                                   fieldOwnersNames: new[] { "B", "C" }),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void MultipleFieldsInDifferentStructsNoError() {
			var code = @"
						abstraction 0 {
                            struct A {
                                string a;
                            }
                            struct B {
                                bool a;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[0];
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void MultipleFieldsOfTheSameTypeNoError() {
			var code = @"
						abstraction 0 {
                            struct A {
                                string a;
                                string a;
                                string a;
                                string[][][] b;
                                string[][][] b;
                                A c;
                                A c;
                                A c;
                                A(string[], A) d;
                                A(string[], A) d;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[0];
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void IndirectFieldTypesCycleNoError() {
			var code = @"
						abstraction 0 {
                            struct A {
								B field;
                            }
							struct B {
								A field2;
							}
						}
						main {
							a := new A;
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[0];
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void SimpleGenericTypeNoError() {
			var code = @"
						abstraction 0 {
                            struct A<T> {
								T t;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[0];
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void SimpleGenericTypeCycle() {
			var code = @"
						abstraction 0 {
                            struct A<T> {
								is A<string>;
								T t;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new[] { new StructDeclarationContainsInheritanceCycles(file: "__TEST__", line: 3, structName: "A<T>") };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void ComplexGenericTypeCycle() {
			var code = @"
						abstraction 0 {
                            struct A<T> {
								is B<A<string>>;
								T t;
                            }
                            struct B<T> {
                                is C<B<A<string>>>;
                                T t;
                            }
                            struct C<T> {
                                is A<T>;
                                T[] arr;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new[] {
				new StructDeclarationContainsInheritanceCycles(file: "__TEST__", line: 3, structName: "A<T>"),
				new StructDeclarationContainsInheritanceCycles("__TEST__", 7, "B<T>"),
				new StructDeclarationContainsInheritanceCycles("__TEST__", 11, "C<T>")
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void SimpleDuplicateFieldsGenerics() {
			var code = @"
						abstraction 0 {
                            struct A<T> {
								T t;
								T t;
                            }
                            struct B<T> {
                                T t;
                                string t;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new[] {
				new MultipleFieldsWithTheSameName(locations: new Location[] {
					                                  new Location("__TEST__", 8),
					                                  new Location("__TEST__", 9)
				                                  },
				                                  name: "t"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void GenericInliningDuplicateFieldsJoinedNoError() {
			var code = @"
						abstraction 0 {
                            struct A {
								is G<string>;
								string f;
                            }
                            struct G<T> {
                                T f;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void GenericInliningCausesDuplicateFields() {
			var code = @"
						abstraction 0 {
                            struct A {
								is B<bool>;
								string t;
                            }
                            struct B<T> {
                                T t;
                                T t;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new[] {
				new InliningResultsInMultipleFieldsWithTheSameName(file: "__TEST__",
				                                                   line: 3,
				                                                   fieldName: "t",
				                                                   fieldOwnersNames: new[] {
					                                                   "A",
					                                                   "B<T>"
				                                                   }),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void InliningOfInvalidType() {
			var code = @"
						abstraction 0 {
                            struct A<T> {
								is string[];
								is A<A<void(bool)>>(string,bool);
                                is T;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new[] {
				new InliningOfNonStructType("__TEST__", 4, "string[]"),
				new InliningOfNonStructType("__TEST__", 5, "A<A<void(bool)>>(string,bool)"),
				new InliningOfNonStructType("__TEST__", 6, "T"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void MultipleStructDefinitionsSimple() {
			var code = @"
						abstraction 0 {
                            struct A {
                                string b;
                            }
                            struct A {
                                bool c;
                                is A;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new MultipleDeclarationsOfStruct(new Location[] {
					                                 new Location("__TEST__", 3),
					                                 new Location("__TEST__", 6)
				                                 },
				                                 "A"),
				new UseOfAmbiguousType("__TEST__", 8, "A")
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void MultipleStructDefinitionsMultipleAbstractions() {
			var code = @"
						abstraction 0 {
                            struct A {
                                string b;
                            }
                        }
                        abstraction 1 {
                            struct A {
                                bool c;
                                is A;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new MultipleDeclarationsOfStruct(new Location[] {
					                                 new Location("__TEST__", 3),
					                                 new Location("__TEST__", 8)
				                                 },
				                                 "A"),
				new UseOfAmbiguousType("__TEST__", 10, "A")
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void MultipleStructDefinitionsDifferentArityNoError() {
			var code = @"
						abstraction 0 {
                            struct A<T> {
                                T b;
                            }
                        }
                        abstraction 1 {
                            struct A<T,T1,T2> {
                                T c;
                                T1 d;
                                T2 g;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void MultipleStructDefinitionsSameArityError() {
			var code = @"
						abstraction 0 {
                            struct A<T> {
                                T b;
                            }
                        }
                        abstraction 1 {
                            struct A<T1> {
                                string c;
                                T1 d;
                                bool g;
                            }
                            struct A<T2> {}
						}
                        abstraction 15 {
                            struct A<T> {}
                        }
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new MultipleDeclarationsOfStruct(new Location[] {
					                                 new Location("__TEST__", 3),
					                                 new Location("__TEST__", 8),
					                                 new Location("__TEST__", 13),
					                                 new Location("__TEST__", 16),
				                                 },
				                                 "A"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void MultipleTypeParametersWithSameName() {
			var code = @"
                        abstraction 1 {
                            struct A<T,T,T,T2> {
                                T c;
                                T d;
                                T2 g;
                            }
						}
";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new MultipleTypeParametersWithTheSameName(new Location[] {
					                                          new Location("__TEST__", 3),
					                                          new Location("__TEST__", 3),
					                                          new Location("__TEST__", 3),
				                                          },
				                                          "T")
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void VisibilityError() {
			var code = @"
        		abstraction 0 {
        		    struct A {
                        B b;
                    }
        		}
                abstraction 1 {
                    struct B {

                    }
                }
        		main {

        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new UseOfUndeclaredType("__TEST__", 4, "B"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void UseOfDuplicateDeclaredStruct() {
			var code = @"
        		abstraction 0 {
        		    struct A { }
                    struct A { }
        		}
        		main {
        		    A a;
        		}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] {
				new MultipleDeclarationsOfStruct(new Location[] {
					                                 new Location("__TEST__", 3),
					                                 new Location("__TEST__", 4)
				                                 },
				                                 "A"),
				new UseOfAmbiguousType("__TEST__", 7, "A"),
			};
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}


		[Test]
		[Parallelizable]
		public void ComplexGenericBug() {
			var code = @"
        		abstraction 0 {

                    struct Pair<A,B> {
                        A a;
                        B b;
                    }

                    struct TrainingData<I,O> {
                        mutable List<Pair<I,O>> data;
                    }

                    struct List<T> {
                        num length;
                        T[] data;
                    }

                    List<R> toList<R>(num integer, R() generator) {
                        return null;
                    }

                    TrainingData<I,O> createTrainingData<I,O>(Pair<I,O>() exampleGenerator, num size) {
                        trainingData := new TrainingData<I,O>;
                        trainingData.data = size.toList( exampleGenerator );
                        return trainingData;
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
		public void MoreComplexGenericBug() {
			var code = @"
        		abstraction 0 {

                    struct Pair<A,B> {
                        A a;
                        B b;
                    }

                    struct TrainingData<I,O> {
                        List<Pair<I,O>> data;
                    }

                    struct List<T> {
                        num length;
                        T[] data;
                    }

                    num calculateError(TrainingData<List<num>,List<num>> trainingData) {
                        mutable List<Pair<List<num>, List<num>>> x;
                        x = trainingData.data;

                        TrainingData<List<num>,List<num>> otherData;
                        y := otherData;
                        return x.data[0].a.length;
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
		public void SimpleNominallyTypedAssignment() {
			var code = @"
        		abstraction 0 {
					nominal struct A { }
					struct B {
						is A
						string a	
					}
        		}
        		main {
					A a = new B	
					A aa = new A
				}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}
		
		[Test]
		[Parallelizable]
		public void SimpleNominallyTypedAssignmentError() {
			var code = @"
        		abstraction 0 {
					nominal struct A { }
					struct B {
						string a	
					}
        		}
        		main {
					A a = new B	
				}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { new TypeMismatchInAssignmentError("__TEST__", 9, "A", "B") };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}
		
		[Test]
		[Parallelizable]
		public void SimpleNominallyTypedFunctionArguments() {
			var code = @"
        		abstraction 0 {

					nominal struct A { }

					struct B {
						string a	
					}
				
					void f(A arg) { }
					
        		}
        		main {
					b := new B { a = 'xxx' }	
					f(b)
				}
        ";
			var (errors, _) = Compiler.Compile(code);
			var expectedErrors = new Error[] { new NoMatchingOverloadForCall("__TEST__", 15, "f(B)") };
			CollectionAssert.AreEquivalent(expectedErrors, errors);
		}
	}

}