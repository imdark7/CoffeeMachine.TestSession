using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CoffeeMachine.Mechanics
{
    public static class SolutionValidations
    {
        public static List<string> ValidateSolution()
        {
            var errors = new List<string>
            {
                ShouldHaveCoffeeMachimeProperty(),
                ShouldNotHavePrivateMethods()
            };
            return errors.Where(x => x != null).ToList();
        }

        private static string ShouldHaveCoffeeMachimeProperty()
        {
            const string propertyName = "CoffeeMachine";
            var properties = typeof(CoffeeMachineTestsTask).GetProperties();
            var property = properties.First(x => x.Name == propertyName);
            if (property == null || property.PropertyType != typeof(ICoffeeMachine))
            {
                return
                    $"Класс {nameof(CoffeeMachineTestsTask)} должен содержать свойство с именем {propertyName},\r\n" +
                    $"с типом {nameof(ICoffeeMachine)}, иначе задание работать не будет.";
            }
            return null;
        }

        private static string ShouldNotHavePrivateMethods()
        {
            var testsClass = typeof(CoffeeMachineTestsTask);
            var nonPublicStaticMethods = testsClass.GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            if (nonPublicStaticMethods.Any())
            {
                var serialized = string.Join(", ", nonPublicStaticMethods.Select(x => x.Name).ToArray());

                return
                    "Класс с тестами не должен содержать непубличных static методов. Это техническое ограничение системы проверки задания.\r\n" +
                    "Замените все такие методы на public static.\r\n" +
                    $"Это были методы: {serialized}";
            }
            return null;
        }
    }
}