using FluentAssertions;
using NUnit.Framework;

namespace CoffeeMachine
{
    public class CoffeeMachineTestsTask
    {
        //это свойство удалять нельзя, без него работать не будет
        public ICoffeeMachine CoffeeMachine { get; protected set; }

        //этот метод удалять нельзя, без него работать не будет.
        //CoffeeMachine меняется на разные некорректные реализации при запуске проверки заданий.
        [SetUp]
        public virtual void SetUp() => CoffeeMachine = new CoffeeMachine();

        //Пример теста
        [Test]
        public void SimpleTest()
        {
            const CoffeeType coffeeType = CoffeeType.Latte;
            var coffeePrice = coffeeType.Price();

            CoffeeMachine.SelectCoffee(coffeeType);
            CoffeeMachine.InsertMoney(coffeePrice);
            CoffeeMachine.SelectSugarLevel(SugarLevel.Low);
            CoffeeMachine.SelectSize(Size.Large);
            CoffeeMachine.Run();
            var coffee = CoffeeMachine.EjectCup();

            coffee.Type.Should().Be(coffeeType);
        }
    }
}