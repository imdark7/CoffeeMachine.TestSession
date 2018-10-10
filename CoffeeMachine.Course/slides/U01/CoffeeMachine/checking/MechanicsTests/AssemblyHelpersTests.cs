using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace CoffeeMachine.Mechanics
{
    public class AssemblyHelpersTests
    {
        private readonly Assembly coffeeMachineAssembly;
        private readonly string[] incorrectImplementations;

        public AssemblyHelpersTests()
        {
            coffeeMachineAssembly = AssemblyHelper.CoffeeMachineAssembly;

            incorrectImplementations = incorrectImplementations = new[]
            {
                "Incorrect_RunA",
                "IncorrectCoffeeMachineAlwaysFails"
            };
        }


        [Test]
        public void GetIncorrectImplementations_ShouldReturnExpectedIncorrectImplementations()
        {
            var allIncorrectImplementationsNames = AssemblyHelper
                .GetIncorrectImplementationTypes<ICoffeeMachine>(coffeeMachineAssembly).Select(x => x.Name);
            var intersection = allIncorrectImplementationsNames.Intersect(incorrectImplementations);
            CollectionAssert.AreEquivalent(
                incorrectImplementations,
                intersection
            );
        }

        [Test]
        public void GetEtalonTests()
        {
            var types = AssemblyHelper.CoffeeMachineAssembly.GetTypes();
            CollectionAssert.Contains(types.Select(x => x.Name), "CoffeeMachineTestsTask");
            var testsType = types.Single(x => x.Name == "CoffeeMachineTestsTask");
            var testNames = testsType.GetMethods().Select(x => x.Name).ToList();
            CollectionAssert.Contains(testNames, "get_CoffeeMachine");
            CollectionAssert.Contains(testNames, "SetUp");
        }

        [Test]
        public void GetIncorrectImplementationsTestsTest()
        {
            var testsClasses = AssemblyHelper.GetIncorrectImplementationTests();
            foreach (var testClass in testsClasses)
            {
                var methods = testClass.GetType().GetMethods();
                var methodsNames = methods.Select(x => x.Name).ToList();
                CollectionAssert.Contains(methodsNames, "get_CoffeeMachine");
                CollectionAssert.Contains(methodsNames, "SetUp");
            }
        }
    }
}