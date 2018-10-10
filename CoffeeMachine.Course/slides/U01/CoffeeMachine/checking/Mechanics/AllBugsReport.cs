using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CoffeeMachine.Mechanics
{
    public static class AllBugsReport
    {
        public static void WriteAllBugsToFile(string path)
        {
            var incorrectImplementations =
                AssemblyHelper.GetIncorrectImplementationTypes<ICoffeeMachine>(AssemblyHelper.CoffeeMachineAssembly)
                    .Select(
                        x => new ImplementationInfo
                        {
                            Name = x.Name,
                            Description = x.GetCustomAttribute<IncorrectImplementationAttribute>().Description,
                            IsSecret = x.HasAttribute<SecretAttribute>()
                        }
                    )
                    .OrderBy(x => x.IsSecret);


            var sb = new StringBuilder();
            sb.AppendLine("Файл генерируется заново при каждом локальном прогоне задания. Не забывайте коммитить!");
            foreach (var incorrectImplementation in incorrectImplementations)
            {
                sb.AppendLine("======");
                sb.AppendLine($"{incorrectImplementation.Name}");
                if (incorrectImplementation.IsSecret) sb.AppendLine("Секретный баг");
                sb.AppendLine($"{incorrectImplementation.Description}");
                sb.AppendLine();
            }

            File.WriteAllText(path, sb.ToString());
        }
    }

    public class ImplementationInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSecret { get; set; }
    }
}