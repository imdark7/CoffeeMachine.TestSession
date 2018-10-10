using System.Linq;

namespace CoffeeMachine.Mechanics
{
    public class ImplementationStatus
    {
        public ImplementationStatus(
            string name,
            FailedTest[] failedTests,
            string bugDescription = null,
            bool isSecret = false)
        {
            Name = name;
            FailedTests = failedTests;
            BugDescription = bugDescription?.EnsureFullStopOnEnd();
            IsSecret = isSecret;
        }

        public bool HasFailedTests => FailedTests.Any();

        public readonly string Name;
        public readonly FailedTest[] FailedTests;
        public readonly string BugDescription;
        public readonly bool IsSecret;
    }
}