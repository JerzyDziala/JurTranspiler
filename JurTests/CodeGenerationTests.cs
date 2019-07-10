using JurTranspiler.compilerSource;
using JurTranspiler.compilerSource.Analysis;
using NUnit.Framework;

namespace JurTranspilerTests {

    [TestFixture]
    public class CodeGenerationTests {

        [Test]
        [Parallelizable]
        public void FieldAccessJoinedField() {
            var code = @"
        		abstraction 0 {
                    struct Dog {
                        string name;
                        num age;
                    }
        		}
        		main {
                    Dog dog = new Dog;
                    dog.name = 'reksio';
                    dog.age = 5;
        		}
        ";
            var (errors, js) = Compiler.Compile(code);
        }
    }

}