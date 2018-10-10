using System;
using System.IO;
using CoffeeMachine.Mechanics;
using NUnit.Framework;

namespace CoffeeMachine.MechanicsTests
{
    public class TestRunReportTests
    {
        [Test]
        public void TestRunReport_NoImplementations()
        {
            var impl = new ImplementationStatus[]
            {
            };
            DoTest(impl,
                expectedSubstrings: new[] {"Найдено багов: 0 из 0"},
                notExpectedSubstrings: new[] {"Найдено секретных багов"});
        }

        [Test]
        public void TestRunReport_SinglePassedImplementation()
        {
            var impl = new[]
            {
                PassedImplementation,
            };
            DoTest(impl,
                expectedSubstrings: new[] {"Найдено багов: 0 из 1"},
                notExpectedSubstrings: new[] {"Найдено секретных багов"});
        }

        [Test]
        public void TestRunReport_ManyPassedImplementations()
        {
            var impls = new[]
            {
                PassedImplementation,
                PassedImplementation,
                PassedImplementation,
            };
            DoTest(impls,
                expectedSubstrings: new[] {$"Найдено багов: 0 из {impls.Length}"},
                notExpectedSubstrings: new[] {"Найдено секретных багов"});
        }

        [Test]
        public void TestRunReport_PassedAndFailedImplementation()
        {
            var impl = new[]
            {
                PassedImplementation,
                FailedImplementation
            };
            DoTest(impl,
                expectedSubstrings: new[]
                {
                    "Найдено багов: 1 из 2",
                    "Баг пойман!",
                    "FailedTest",
                    "Failed impl bug description",
                    "Test fail message"
                },
                notExpectedSubstrings: new[] {"Найдено секретных багов"});
        }

        [Test]
        public void TestRunReport_PassedSecretAndFailedSecretImplementations()
        {
            var impl = new[]
            {
                PassedSecretImplementation,
                FailedSecretImplementation
            };
            DoTest(impl, expectedSubstrings: new[]
            {
                "Найдено багов: 0 из 0",
                "Найдено секретных багов: 1",
                "Секретный баг пойман!",
                "FailedTest",
                "Failed impl bug description",
                "Test fail message"
            });
        }

        [Test]
        public void TestRunReport_ManyFailedSecretImplementations()
        {
            var impl = new[]
            {
                FailedSecretImplementation,
                FailedSecretImplementation,
                FailedSecretImplementation,
            };
            DoTest(impl, expectedSubstrings: new[]
            {
                "Найдено багов: 0 из 0",
                $"Найдено секретных багов: {impl.Length}",
                "Секретный баг пойман!",
                "FailedTest",
                "Failed impl bug description",
                "Test fail message"
            });
        }

        [Test]
        public void TestRunReport_DifferentImplementations()
        {
            var impl = new[]
            {
                PassedImplementation,
                PassedImplementation,
                FailedImplementation,
                FailedImplementation,
                PassedSecretImplementation,
                FailedSecretImplementation,
                FailedSecretImplementation,
                FailedSecretImplementation,
            };
            DoTest(impl, expectedSubstrings: new[]
            {
                "Найдено багов: 2 из 4",
                $"Найдено секретных багов: 3",
                "Баг пойман!",
                "Секретный баг пойман!",
                "FailedTest",
                "Failed impl bug description",
                "Test fail message"
            });
        }

        private static void DoTest(ImplementationStatus[] impl,
            string[] expectedSubstrings, string[] notExpectedSubstrings = null)
        {
            const string delimiter = "\r\n<===================>\r\n";
            var output = "";
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                new TestRunReport(impl)
                    .WriteToConsole();
                output = sw.ToString(); //может не поместиться в строку, если отчет будет длинный
            }

            Console.SetOut(TestContext.Out);
            Console.WriteLine("TestRunReport выдал: \r\n" + delimiter + output + delimiter);

            foreach (var expectedSubstring in expectedSubstrings)
            {
                Assert.That(output, Does.Contain(expectedSubstring));
            }

            if (notExpectedSubstrings != null)
                foreach (var notExpectedSubstring in notExpectedSubstrings)
                {
                    Assert.That(output, Does.Not.Contain(notExpectedSubstring));
                }
        }

        private static readonly ImplementationStatus PassedImplementation =
            new ImplementationStatus("PassedImplementation", new FailedTest[0], "Passed impl bug description");

        private static readonly ImplementationStatus PassedSecretImplementation =
            new ImplementationStatus("PassedImplementation", new FailedTest[0], "Passed impl bug description",
                isSecret: true);

        private static readonly ImplementationStatus FailedImplementation = new ImplementationStatus(
            "FailedImplementation", new[]
            {
                new FailedTest("FailedTest", "Test fail message"),
            }, "Failed impl bug description");

        private static readonly ImplementationStatus FailedSecretImplementation = new ImplementationStatus(
            "FailedImplementation", new[]
            {
                new FailedTest("FailedTest", "Test fail message"),
            }, "Failed impl bug description", isSecret: true);
    }
}