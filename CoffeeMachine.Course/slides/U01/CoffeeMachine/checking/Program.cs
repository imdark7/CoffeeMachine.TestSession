using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CoffeeMachine.Mechanics;
using NUnit.Framework.Api;

namespace CoffeeMachine
{
    public static class Program
    {
        //todo: как-то безопаснее включать этот флаг, чтобы никто никогда не забыл его снять при выкладывании
        private const bool ShouldPrintDebugInfo = false;

        private static readonly string AllBugsReportPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "../../../../../../AllIncorrectImplementationsReport.txt");

        public static void Main(string[] args)
        {
            var errors = SolutionValidations.ValidateSolution();
            if (errors.Count != 0)
            {
                var serialized = string.Join("\r\n", errors);
                throw new Exception($"В задании были найдены ошибки. Их нужно исправить:\r\n{serialized}");
            }

            var testRunner = TestRunningHelpers.CreateTestRunner();
            if (!TestsAreValid(testRunner)) return;
            var implementationStatuses = RunIncorrectImplementationsTests(testRunner);
            new TestRunReport(implementationStatuses, ShouldPrintDebugInfo).WriteToConsole();

            try
            {
                AllBugsReport.WriteAllBugsToFile(AllBugsReportPath);
            }
            catch (Exception)
            {
                //При запуске в юлерне напечатать не получится, потому что там нельзя. Ну и ладно.
            }
        }

        private static bool TestsAreValid(ITestAssemblyRunner testRunner)
        {
            Console.WriteLine("Проверяем, что все тесты проходят на эталонной CoffeeMachine...");
            var failed = TestRunningHelpers.GetFailedTests(testRunner, typeof(CoffeeMachineTestsTask)).ToList();
            if (failed.Any())
            {
                Console.WriteLine(
                    "Тесты не в порядке. На эталонной CoffeeMachine не прошли: " +
                    string.Join(", ", failed.Select(x => x.ToString(ShouldPrintDebugInfo))));
                return false;
            }

            Console.WriteLine("Тесты в порядке.");
            return true;
        }

        private static ImplementationStatus[] RunIncorrectImplementationsTests(ITestAssemblyRunner testRunner)
        {
            Console.WriteLine("Ловим баги...\r\n");
            var incorrectImplementations =
                AssemblyHelper.GetIncorrectImplementationTypes<ICoffeeMachine>(AssemblyHelper.CoffeeMachineAssembly);
            return GetIncorrectImplementationsResults(testRunner, incorrectImplementations).ToArray();
        }

        private static IEnumerable<ImplementationStatus> GetIncorrectImplementationsResults(
            ITestAssemblyRunner testRunner, IEnumerable<Type> implementations)
        {
            var implTypeToTestsType = AssemblyHelper.GetIncorrectImplementationTests()
                .ToDictionary(t =>
                {
                    t.SetUp();
                    return t.CoffeeMachine.GetType();
                }, t => t.GetType());
            foreach (var implementation in implementations)
            {
                var isSecret = implementation.HasAttribute<SecretAttribute>();
                var failed = TestRunningHelpers.GetFailedTests(testRunner,
                        implTypeToTestsType[implementation])
                    .ToArray();
                yield return new ImplementationStatus(
                    implementation.Name,
                    failed,
                    IncorrectImplementationAttribute.GetDescription(implementation),
                    isSecret);
            }
        }
    }
}