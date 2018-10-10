using System.Linq;
using System.Reflection;
using CoffeeMachine.Mechanics;
using NUnit.Framework;
using NUnit.Framework.Api;

namespace CoffeeMachine.MechanicsTests
{
    public class TestRunningHelpersTests
    {
        public TestRunningHelpersTests()
        {
            testRunner = new NUnitTestAssemblyRunner(new DefaultTestAssemblyBuilder());
            testRunner.Load(Assembly.GetExecutingAssembly(), TestRunningHelpers.TestRunnerSettings);
        }


        [Test]
        public void GetFailedTestsTest()
        {
            var failedTests = TestRunningHelpers.GetFailedTests(testRunner, typeof(DummyTests)).ToList();
            Assert.AreEqual(2, failedTests.Count);
            var firstFailedTest = failedTests.Single(x => x.Name == "TestWhichFails1");
            Assert.AreEqual("Все ок, он и должен падать.", firstFailedTest.FailMessage);
            var secondFailedTest = failedTests.Single(x => x.Name == "TestWhichFails2");
            Assert.AreEqual("Все ок, он и должен падать.", secondFailedTest.FailMessage);
        }


        private readonly ITestAssemblyRunner testRunner;
    }
}