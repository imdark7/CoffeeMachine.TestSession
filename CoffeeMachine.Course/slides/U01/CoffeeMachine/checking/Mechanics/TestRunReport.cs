using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeMachine.Mechanics
{
    public class TestRunReport
    {
        public TestRunReport(ImplementationStatus[] implementationStatuses, bool printDebugInfo = false)
        {
            _implementationStatuses = implementationStatuses;
            _printDebugInfo = printDebugInfo;

            var notSecretBugs = _implementationStatuses.Where(x => !x.IsSecret).ToArray();
            TotalNotSecretBugs = notSecretBugs.Length;
            FoundNotSecretBugs = notSecretBugs.Count(x => x.HasFailedTests);

            var secretBugs = _implementationStatuses.Where(x => x.IsSecret).ToArray();
            FoundSecretBugs = secretBugs.Count(x => x.HasFailedTests);
        }

        public void WriteToConsole()
        {
            WriteReportSummaryToConsole();
            foreach (var status in OrderBySecret(_implementationStatuses))
            {
                WriteImplementationStatusToConsole(status);
            }
        }


        private void WriteReportSummaryToConsole()
        {
            Console.WriteLine($"Найдено багов: {FoundNotSecretBugs} из {TotalNotSecretBugs}");
            if (FoundSecretBugs != 0)
            {
                Console.WriteLine($"Найдено секретных багов: {FoundSecretBugs}");
            }

            Console.WriteLine();
            Console.WriteLine("--------");
        }

        private void WriteImplementationStatusToConsole(ImplementationStatus status)
        {
            if (status.HasFailedTests)
            {
                var paddedName = status.Name.PadRight(20, ' ');
                var caughtBug = status.IsSecret ? "Секретный баг пойман!" : "Баг пойман!";
                var failedTests = status.FailedTests;
                var limit = 3;
                if (_printDebugInfo) limit = 999;
                var allFailedTestsString = $"{failedTests.Select(x => x.Name).SerializeNoMoreThan(limit, "тестов.")}";

                var result = paddedName + $" -- {caughtBug} Описание: {status.BugDescription}\r\n" +
                             $" Поймали на тесте: {failedTests.First().ToString(_printDebugInfo)}\r\n"
                             + "Все упавшие тесты: "
                             + allFailedTestsString;

                Console.WriteLine(result);
                Console.WriteLine();
            }
        }

        private static IEnumerable<ImplementationStatus> OrderBySecret(
            IEnumerable<ImplementationStatus> statuses)
        {
            return statuses.OrderByDescending(x => x.IsSecret);
        }

        private readonly ImplementationStatus[] _implementationStatuses;
        private readonly bool _printDebugInfo;
        private readonly int TotalNotSecretBugs;
        private readonly int FoundNotSecretBugs;
        private readonly int FoundSecretBugs;
    }
}