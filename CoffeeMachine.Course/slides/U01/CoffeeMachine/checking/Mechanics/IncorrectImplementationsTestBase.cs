using NUnit.Framework;
using System;
using System.Reflection;

namespace CoffeeMachine.Mechanics
{
    public class IncorrectImplementationsTestBase : CoffeeMachineTestsTask
    {
        public override void SetUp()
        {
            base.SetUp();

            var ns = AssemblyHelper.IncorrectImplementationsNamespace;
            var implTypeName = ns + "." + GetType().Name.Replace("_Tests", "");
            var assembly = Assembly.GetExecutingAssembly();
            if (assembly.GetType(implTypeName) == null)
                Assert.Fail("no type {0}", implTypeName);
            var type = assembly.GetType(implTypeName);
            CoffeeMachine = (ICoffeeMachine) type.GetConstructor(new Type[0]).Invoke(new object[0]);
        }
    }
}