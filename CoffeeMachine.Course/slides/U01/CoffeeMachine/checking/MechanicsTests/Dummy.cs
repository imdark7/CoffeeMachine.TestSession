using NUnit.Framework;

namespace CoffeeMachine.MechanicsTests
{
    public static class Dummy
    {
        public static void Fail(string assertMsg)
        {
            Assert.Fail(assertMsg);
        }

        public static void Pass()
        {
            return;
        }
    }

    public class DummyTests
    {
        [Test, Description("Все ок, он и должен падать")]
        public void TestWhichFails1()
        {
            Dummy.Fail("Все ок, он и должен падать");
        }

        [Test, Description("Все ок, он и должен падать")]
        public void TestWhichFails2()
        {
            Dummy.Fail("Все ок, он и должен падать");
        }

        [Test]
        public void TestWhichPasses()
        {
            Dummy.Pass();
        }
    }
}