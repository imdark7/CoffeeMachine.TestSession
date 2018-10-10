using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace CoffeeMachine.Mechanics
{
    public class IncorrectTests
    {
        [TestFixture]
        public class GenerateIncorrectTests
        {
            [Test]
            public void Generate()
            {
                var impls =
                    AssemblyHelper.GetIncorrectImplementationTypes<ICoffeeMachine>(AssemblyHelper.CoffeeMachineAssembly).ToList();
                AssertIncorrectImplementationsHaveNoVirtualMethods(impls);
                var code = string.Join(Environment.NewLine,
                    impls.Select(imp =>
                        $"public class {imp.Name}_Tests : {nameof(IncorrectImplementationsTestBase)} {{}}")
                );
                Console.WriteLine(code);
            }

            [Test]
            public void CheckAllTestsAreInPlace()
            {
                var implTypes =
                    AssemblyHelper.GetIncorrectImplementationTypes<ICoffeeMachine>(AssemblyHelper.CoffeeMachineAssembly);
                var testedImpls = AssemblyHelper.GetIncorrectImplementationTests();
                var x = testedImpls
                .Select(t =>
                {
                    t.SetUp();
                    return t.CoffeeMachine.GetType();
                })
                .ToArray();

                foreach (var impl in implTypes)
                {
                    Assert.NotNull(x.SingleOrDefault(t => t.FullName == impl.FullName),
                        "Single implementation of tests for {0} not found. Regenerate tests with test above!",
                        impl.FullName);
                }
            }
        }

        private static void AssertIncorrectImplementationsHaveNoVirtualMethods(IEnumerable<Type> incorrectImplementations)
        {
            foreach (var incorrectImplementation in incorrectImplementations)
            {
                var virtualMethods = incorrectImplementation
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .Where(x => x.IsVirtual)
                    .Where(x => x.GetBaseDefinition().DeclaringType == x.DeclaringType)
                    .ToArray();
                if (virtualMethods.Any())
                    throw new Exception(
                        $"В реализации {incorrectImplementation} есть virtual методы: {string.Join(", ", virtualMethods.Select(x => x.Name))}. " +
                        $"Замени virtual на override или ничего работать не будет.");
            }
        }

        #region Generated

        public class Incorrect_RunA_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_RunB_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_RunC_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_RunD_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_EjectCupA_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_EjectCupB_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_GetChangeA_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_GetChangeB_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_GetChangeC_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_GetChangeD_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_SelectSizeA_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_SelectSizeB_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_SelectSizeC_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_InsertMoneyA_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_InsertMoneyB_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_InsertMoneyC_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_InsertMoneyD_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_InsertMoneyE_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_InsertMoneyF_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_InsertMoneyG_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_SelectSugarA_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_SelectSugarB_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_SelectSugarC_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_SelectCoffeeA_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_SelectCoffeeB_Tests : IncorrectImplementationsTestBase { }
        public class Incorrect_SelectCoffeeC_Tests : IncorrectImplementationsTestBase { }
        public class IncorrectCoffeeMachineAlwaysFails_Tests : IncorrectImplementationsTestBase { }

        #endregion
    }
}