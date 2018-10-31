using System;
using CoffeeMachine.IncorrectImplementations;
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

        [Test]
        public void TestMainCase()
        {

            const CoffeeType coffeeType = CoffeeType.Latte;
            var coffeePrice = coffeeType.Price();

            CoffeeMachine.SelectCoffee(coffeeType);
            CoffeeMachine.InsertMoney(coffeePrice + 50);
            CoffeeMachine.SelectSugarLevel(SugarLevel.Low);
            CoffeeMachine.SelectSize(Size.Large);
            CoffeeMachine.Run();
            var coffee = CoffeeMachine.EjectCup();
            coffee.Type.Should().Be(coffeeType);
            coffee.Sugar.Should().Be(SugarLevel.Low);
            coffee.Size.Should().Be(Size.Large);
            CoffeeMachine.GetChange().Should().Be(50);
        }

        [Test]
        public void TestChangeToMe()
        {

            const CoffeeType coffeeType = CoffeeType.Latte;
            var coffeePrice = coffeeType.Price();

            CoffeeMachine.SelectCoffee(coffeeType);
            CoffeeMachine.InsertMoney(coffeePrice);
            CoffeeMachine.GetChange();

            try
            {
                CoffeeMachine.SelectSugarLevel(SugarLevel.Low);
            }
            catch (CoffeeMachineException e)
            {
                e.Message.Should().Be("You can't select sugar level. First, select coffee type and insert money");
            }


        }

        [Test]
        public void TestLowMoney()
        {

            const CoffeeType coffeeType = CoffeeType.Latte;
            CoffeeMachine.SelectCoffee(coffeeType);
            CoffeeMachine.InsertMoney(1);
            try
            {
                CoffeeMachine.SelectSize(Size.Large);
            }
            catch (CoffeeMachineException e)
            {
                e.Message.Should().Be("You can't select cup size. First, select coffee type and insert money");
            }

        }


        [Test]
        public void Testorder1()
        {

            const CoffeeType coffeeType = CoffeeType.Latte;
            var coffeePrice = coffeeType.Price();

            CoffeeMachine.SelectCoffee(coffeeType);
            CoffeeMachine.InsertMoney(coffeePrice + 50);
            CoffeeMachine.SelectSize(Size.Large);
            CoffeeMachine.SelectSugarLevel(SugarLevel.Low);
            CoffeeMachine.Run();
            var coffee = CoffeeMachine.EjectCup();
            coffee.Type.Should().Be(coffeeType);
            coffee.Sugar.Should().Be(SugarLevel.Low);
            coffee.Size.Should().Be(Size.Large);
            CoffeeMachine.GetChange().Should().Be(50);
        }

        [Test]
        public void TestUnknown3()
        {

            const CoffeeType coffeeType = CoffeeType.Latte;
            var coffeePrice = coffeeType.Price();

            CoffeeMachine.SelectCoffee(coffeeType);
            CoffeeMachine.InsertMoney(coffeePrice + 50);
            CoffeeMachine.SelectSugarLevel(SugarLevel.Unknown);
            CoffeeMachine.SelectSize(Size.Large);
            try
            {
                CoffeeMachine.Run();
            }
            catch (CoffeeMachineException e)
            {
                e.Message.Should().Be("You can't start making coffee. First, select Coffee Type, Size and Sugar level");
            }

        }

        [Test]
        public void TestUnknown1()
        {

            const CoffeeType coffeeType = CoffeeType.Unknown;
            try
            {
                coffeeType.Price();
            }
            catch (ArgumentOutOfRangeException e)
            {
                e.Message.Should().Contain("System.ArgumentOutOfRangeException");
            }
        }


        [Test]
        public void TestUnknown2()
        {
            const CoffeeType coffeeType = CoffeeType.Unknown;


            try
            {
                CoffeeMachine.SelectCoffee(coffeeType);
            }
            catch (CoffeeMachineException e)
            {
                e.Message.Should().Contain("You must select correct coffee type!");
            }
        }

        [Test]
        public void TestEjectCup1()
        {
            try
            {
                CoffeeMachine.EjectCup();
            }
            catch (CoffeeMachineException e)
            {
                e.Message.Should().Be("You can't eject cup. Coffee is not ready yet.");
            }

        }

        [Test]
        public void TestEjectCup2()
        {
            try
            {
                const CoffeeType coffeeType = CoffeeType.Latte;
                var coffeePrice = coffeeType.Price();
                CoffeeMachine.SelectCoffee(coffeeType);
                CoffeeMachine.InsertMoney(coffeePrice + 50);
                CoffeeMachine.SelectSugarLevel(SugarLevel.Low);
                CoffeeMachine.SelectSize(Size.Large);
                CoffeeMachine.EjectCup();
            }
            catch (CoffeeMachineException e)
            {
                e.Message.Should().Be("You can't eject cup. Coffee is not ready yet.");
            }

        }

        [Test]
        public void TestMany()
        {
            for (int i = 0; i < 1000; i++)
            {
                const CoffeeType coffeeType = CoffeeType.Latte;
                var coffeePrice = coffeeType.Price();

                CoffeeMachine.SelectCoffee(coffeeType);
                CoffeeMachine.InsertMoney(coffeePrice + 50);
                CoffeeMachine.SelectSugarLevel(SugarLevel.Low);
                CoffeeMachine.SelectSize(Size.Large);
                CoffeeMachine.Run();
                var coffee = CoffeeMachine.EjectCup();
                coffee.Type.Should().Be(coffeeType);
                coffee.Sugar.Should().Be(SugarLevel.Low);
                coffee.Size.Should().Be(Size.Large);
                CoffeeMachine.GetChange().Should().Be(50);
            }

        }


        [Test]
        public void TestReselect()
        {
            CoffeeMachine.InsertMoney(CoffeeType.Latte.Price());
            CoffeeMachine.SelectCoffee(CoffeeType.Latte);
            CoffeeMachine.SelectSugarLevel(SugarLevel.Low);
            CoffeeMachine.SelectSize(Size.Large);

            CoffeeMachine.SelectCoffee(CoffeeType.Cappucchino);
            CoffeeMachine.SelectSugarLevel(SugarLevel.None);
            CoffeeMachine.SelectSize(Size.Small);
            CoffeeMachine.Run();
            var coffee = CoffeeMachine.EjectCup();
            coffee.Type.Should().Be(CoffeeType.Cappucchino);
            coffee.Sugar.Should().Be(SugarLevel.None);
            coffee.Size.Should().Be(Size.Small);
            CoffeeMachine.GetChange().Should().Be(10);
        }

        [Test]
        public void Test1()
        {

            const CoffeeType coffeeType = CoffeeType.Latte;
            var coffeePrice = coffeeType.Price();

            CoffeeMachine.SelectCoffee(coffeeType);
            CoffeeMachine.InsertMoney(coffeePrice + 50);
            CoffeeMachine.SelectSugarLevel(SugarLevel.Low);
            CoffeeMachine.SelectSize(Size.Large);
            CoffeeMachine.Run();

            try
            {
                CoffeeMachine.InsertMoney(coffeePrice + 50);
            }
            catch (CoffeeMachineException e)
            {
                e.Message.Should().Be("You can't insert money. First, take a cup from the coffee machine.");
            }

        }

        [Test]
        public void Test2()
        {

            const CoffeeType coffeeType = CoffeeType.Latte;
            var coffeePrice = coffeeType.Price();

            CoffeeMachine.SelectCoffee(coffeeType);
            CoffeeMachine.InsertMoney(coffeePrice + 50);
            CoffeeMachine.SelectSugarLevel(SugarLevel.Low);
            CoffeeMachine.SelectSize(Size.Large);
            CoffeeMachine.Run();

            try
            {
                CoffeeMachine.SelectCoffee(coffeeType);
            }
            catch (CoffeeMachineException e)
            {
                e.Message.Should().Be("You can't select coffee type. First, take a cup from the coffee machine");
            }
        }

        [Test]
        public void TestRunBefore()
        {

            try
            {
                CoffeeMachine.Run();
            }
            catch (CoffeeMachineException e)
            {
                e.Message.Should().Be("You can't start making coffee. First, select Coffee Type, Size and Sugar level");
            }


        }


        [Test]
        public void TestChangeAfterRun()
        {
            const CoffeeType coffeeType = CoffeeType.Americano;
            var coffeePrice = coffeeType.Price();

            CoffeeMachine.SelectCoffee(coffeeType);
            CoffeeMachine.InsertMoney(coffeePrice);
            CoffeeMachine.SelectSugarLevel(SugarLevel.Low);
            CoffeeMachine.SelectSize(Size.Large);
            CoffeeMachine.Run();
            var change = CoffeeMachine.GetChange();
            change.Should().Be(0);
            var coffee = CoffeeMachine.EjectCup();
            coffee.Type.Should().Be(coffeeType);
            coffee.Sugar.Should().Be(SugarLevel.Low);
            coffee.Size.Should().Be(Size.Large);
        }


        [Test]
        public void TestChangeAfterCustom()
        {

            const CoffeeType coffeeType = CoffeeType.Espresso;
            var coffeePrice = coffeeType.Price();

            CoffeeMachine.SelectCoffee(coffeeType);
            CoffeeMachine.InsertMoney(coffeePrice);
            CoffeeMachine.SelectSugarLevel(SugarLevel.High);
            CoffeeMachine.SelectSize(Size.Normal);
            var change = CoffeeMachine.GetChange();
            change.Should().Be(coffeePrice);
            try
            {
                CoffeeMachine.Run();
            }
            catch (CoffeeMachineException e)
            {
                e.Message.Should().Be("You can't start making coffee. First, select Coffee Type, Size and Sugar level");
            }

        }

        [Test]
        public void TestGetChange()
        {
            CoffeeMachine.InsertMoney(100);
            CoffeeMachine.GetChange().Should().Be(100);
            CoffeeMachine.GetChange().Should().Be(0);
        }

        [Test]
        public void TestChangeSumm()
        {
            CoffeeMachine.InsertMoney(100);
            CoffeeMachine.InsertMoney(100);
            CoffeeMachine.GetChange().Should().Be(200);
            CoffeeMachine.GetChange().Should().Be(0);
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void TestPositiveMoney(int money)
        {
            try
            {
                CoffeeMachine.InsertMoney(money);
            }
            catch (CoffeeMachineException e)
            {
                e.Message.Should().Be("Amount must be a positive number");
            }
        }


    }
}