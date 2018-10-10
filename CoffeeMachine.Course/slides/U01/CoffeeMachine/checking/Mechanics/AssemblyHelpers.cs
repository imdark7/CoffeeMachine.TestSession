using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CoffeeMachine.Mechanics
{
    public static class AssemblyHelper
    {
        public static readonly string IncorrectImplementationsNamespace =
            "CoffeeMachine.IncorrectImplementations";

        public static readonly Assembly CoffeeMachineAssembly = Assembly.GetExecutingAssembly();


        public static IEnumerable<Type> GetIncorrectImplementationTypes<T>(Assembly assembly)
        {
            return
                assembly
                    .GetInheritorsOf<T>()
                    .Where(t => t.HasAttribute<IncorrectImplementationAttribute>())
                    .OrderBy(t => t.Name.Length).ThenBy(t => t.Name)
                    .ToArray();
        }

        public static IEnumerable<IncorrectImplementationsTestBase> GetIncorrectImplementationTests()
        {
            return GetInheritorsOf<IncorrectImplementationsTestBase>(CoffeeMachineAssembly)
                .Select(t => t.GetConstructor(new Type[0]))
                .Select(t => t.Invoke(new object[0]))
                .Cast<IncorrectImplementationsTestBase>()
                .ToArray();
        }

        public static bool HasAttribute<TAttribute>(this Type method) where TAttribute : Attribute
        {
            return method.GetCustomAttributes(typeof(TAttribute), true).Any();
        }


        private static IEnumerable<Type> GetInheritorsOf<T>(this Assembly assembly)
        {
            var baseType = typeof(T);
            return assembly.GetTypes()
                .Where(baseType.IsAssignableFrom)
                .Where(t => t != baseType && !t.IsAbstract && !t.IsInterface)
                .ToArray();
        }
    }
}