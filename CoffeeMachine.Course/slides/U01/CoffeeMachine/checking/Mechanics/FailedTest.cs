namespace CoffeeMachine.Mechanics
{
    public class FailedTest
    {
        public readonly string Name;
        public readonly string FailMessage;
        public readonly string AdditionalFailInfo;

        public FailedTest(string name, string failMessage = null, string additionalFailInfo = null)
        {
            Name = name;
            if (failMessage != null)
                FailMessage = failMessage
                    .Replace('\r', ' ')
                    .Replace('\n', ' ')
                    .TrimWhitespaces()
                    .EnsureFullStopOnEnd();
            this.AdditionalFailInfo = additionalFailInfo;
        }

        public string ToString(bool printAdditionalInfo = false)
        {
            var result = $"{Name}, упал с сообщением:\r\n{FailMessage}";
            if (printAdditionalInfo) result += $"\r\n\r\n{AdditionalFailInfo}";
            return result;
        }
    }
}