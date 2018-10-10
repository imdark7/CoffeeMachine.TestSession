using System;
using System.Reflection;

namespace CoffeeMachine.Mechanics
{
    public class IncorrectImplementationAttribute : Attribute
    {
        public IncorrectImplementationAttribute(string description)
        {
            Description = description;
        }

        public string Description;

        public static string GetDescription(Type type)
        {
            if (type.HasAttribute<IncorrectImplementationAttribute>())
            {
                return type.GetCustomAttribute<IncorrectImplementationAttribute>()
                    .Description;
            }

            return null; //если не покажем описания бага, то обидно. Но ничего страшного.
        }
    }
}