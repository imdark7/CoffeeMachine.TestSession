using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using NUnit.Framework.Api;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace CoffeeMachine.Mechanics
{
    public static class TestRunningHelpers
    {
        public static readonly Dictionary<string, object> TestRunnerSettings =
            new Dictionary<string, object> { { "StopOnError", false } };

        public static NUnitTestAssemblyRunner CreateTestRunner()
        {
            var testRunner = new NUnitTestAssemblyRunner(new DefaultTestAssemblyBuilder());
            testRunner.Load(AssemblyHelper.CoffeeMachineAssembly, TestRunnerSettings);
            return testRunner;
        }

        public static IEnumerable<FailedTest> GetFailedTests(ITestAssemblyRunner testRunner, Type testsType)
        {
            var testResult = RunTests(testRunner, testsType);
            Debug.Assert(testResult != null);
            return GetFailedTestsFromTestResult(testResult);
        }

        private static IEnumerable<FailedTest> GetFailedTestsFromTestResult(ITestResult testResult)
        {
            if (!testResult.HasChildren)
            {
                if (testResult.ResultState.Status == TestStatus.Failed)
                    yield return new FailedTest(testResult.Name, testResult.Message, testResult.StackTrace);
            }

            foreach (var children in testResult.Children)
                foreach (var failedTest in GetFailedTestsFromTestResult(children))
                    yield return failedTest;
        }

        private static ITestResult RunTests(ITestAssemblyRunner testRunner, Type testsType)
        {
            var testFilter = new ClassNameFilter(testsType.FullName);
            return testRunner.Run(TestListener.NULL, testFilter);
        }
    }

    /* Based on internal
     classes ClassNameFilter and ValueMatchFilter from NUnit */
    [Serializable]
    internal class ClassNameFilter : TestFilter
    {
        /// <summary>
        /// Returns the value matched by the filter - used for testing
        /// </summary>
        private string ExpectedValue { get; }

        /// <summary>Indicates whether the value is a regular expression</summary>
        public bool IsRegex { get; set; }

        /// <summary>Construct a ValueMatchFilter for a single value.</summary>
        /// <param name="expectedValue">The value to be included.</param>
        public ClassNameFilter(string expectedValue)
        {
            ExpectedValue = expectedValue;
        }

        /// <inheritdoc />
        /// <summary>Adds an XML node</summary>
        /// <param name="parentNode">Parent node</param>
        /// <param name="recursive">True if recursive</param>
        /// <returns>The added XML node</returns>
        public override TNode AddToXml(TNode parentNode, bool recursive)
        {
            var tnode = parentNode.AddElement(ElementName, ExpectedValue);
            if (IsRegex)
                tnode.AddAttribute("re", "1");
            return tnode;
        }

        /// <summary>Match the input provided by the derived class</summary>
        /// <param name="input">The value to be matched</param>
        /// <returns>True for a match, false otherwise.</returns>
        private bool Match(string input)
        {
            if (!IsRegex)
                return ExpectedValue == input;
            if (input != null)
                return new Regex(ExpectedValue).IsMatch(input);
            return false;
        }

        /// <inheritdoc />
        /// <summary>Match a test against a single value.</summary>
        public override bool Match(ITest test)
        {
            if (!test.IsSuite || test is ParameterizedMethodSuite || test.ClassName == null)
                return false;
            return Match(test.ClassName);
        }

        private string ElementName => "class";
    }
}