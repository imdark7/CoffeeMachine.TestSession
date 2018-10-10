using FluentAssertions;
using NUnit.Framework;

namespace CoffeeMachine
{
    [TestFixture]
    public class CoffeeMachineTestsTaskSolution
    {
        public ICoffeeMachine CoffeeMachine { get; protected set; }

        [SetUp]
        public virtual void SetUp() => CoffeeMachine = new CoffeeMachine();

        [Test]
        public void CorrectWorkTest()
        {
            const int money = 500;
            const CoffeeType type = CoffeeType.Cappucchino;
            const SugarLevel sugarLevel = SugarLevel.Medium;
            const Size size = Size.Large;
            CoffeeMachine.SelectCoffee(type);
            CoffeeMachine.InsertMoney(money);
            CoffeeMachine.WorkStatus.Should().Be(WorkStatus.Customization);

            CoffeeMachine.SelectSugarLevel(sugarLevel);
            CoffeeMachine.SelectSize(size);
            CoffeeMachine.WorkStatus.Should().Be(WorkStatus.ReadyForMakingCoffee);

            CoffeeMachine.Run();
            CoffeeMachine.WorkStatus.Should().Be(WorkStatus.TakeYourCoffee);

            CoffeeMachine.GetChange().Should().Be(money - type.Price());

            var coffee = CoffeeMachine.EjectCup();
            coffee.Should().BeEquivalentTo(new Coffee(type, sugarLevel, size));
            CoffeeMachine.WorkStatus.Should().Be(WorkStatus.InsertMoneyOrSelectCoffee);
            CoffeeMachine.Size.Should().Be(Size.Unknown);
            CoffeeMachine.SugarLevel.Should().Be(SugarLevel.Unknown);
            CoffeeMachine.CoffeeType.Should().Be(CoffeeType.Unknown);
        }

        [Test]
        public void DefaultValuesTest()
        {
            CoffeeMachine.Size.Should().Be(Size.Unknown);
            CoffeeMachine.WorkStatus.Should().Be(WorkStatus.InsertMoneyOrSelectCoffee);
            CoffeeMachine.Balance.Should().Be(0);
            CoffeeMachine.SugarLevel.Should().Be(SugarLevel.Unknown);
            CoffeeMachine.CoffeeType.Should().Be(CoffeeType.Unknown);
        }

        [Test]
        public void EjectCupWhenCoffeeIsNotReadyYet()
        {
            Assert.Throws<CoffeeMachineException>(() => CoffeeMachine.EjectCup());
        }

        [Test]
        public void InsertMoneyWhenCupIsNotEjected()
        {
            CoffeeMachine.SelectCoffee(CoffeeType.Cappucchino);
            CoffeeMachine.InsertMoney(100);
            CoffeeMachine.SelectSugarLevel(SugarLevel.High);
            CoffeeMachine.SelectSize(Size.Large);
            CoffeeMachine.Run();

            Assert.Throws<CoffeeMachineException>(() => CoffeeMachine.InsertMoney(100));
        }

        [Test]
        public void InsertMoneyBeforeSelectCoffeeSelect()
        {
            CoffeeMachine.InsertMoney(100);
            CoffeeMachine.SelectCoffee(CoffeeType.Cappucchino);
            CoffeeMachine.SelectSugarLevel(SugarLevel.High);
            CoffeeMachine.SelectSize(Size.Large);
            CoffeeMachine.Run();

            Assert.Throws<CoffeeMachineException>(() => CoffeeMachine.InsertMoney(100));
        }

        [Test]
        public void InsertNegativeAmount()
        {
            CoffeeMachine.InsertMoney(100);
            Assert.Throws<CoffeeMachineException>(() => CoffeeMachine.InsertMoney(-50));
        }

        [Test]
        public void MakeCoffeeWithoutEnoughMoney()
        {
            CoffeeMachine.SelectCoffee(CoffeeType.Americano);
            CoffeeMachine.InsertMoney(10);
            CoffeeMachine.WorkStatus.Should().Be(WorkStatus.InsertMoneyOrSelectCoffee);
        }

        [Test]
        public void BalanceAfterGetChange()
        {
            const int money = 150;
            const CoffeeType type = CoffeeType.Latte;
            CoffeeMachine.SelectCoffee(type);
            CoffeeMachine.InsertMoney(money);
            CoffeeMachine.SelectSize(Size.Normal);
            CoffeeMachine.SelectSugarLevel(SugarLevel.Low);
            CoffeeMachine.Run();
            var expectedChange = money - type.Price();
            CoffeeMachine.Balance.Should().Be(expectedChange);
            CoffeeMachine.GetChange().Should().Be(expectedChange);
            CoffeeMachine.Balance.Should().Be(0);
        }

        [Test]
        public void StatusAfterGetChange()
        {
            const int money = 150;
            const CoffeeType type = CoffeeType.Latte;
            CoffeeMachine.SelectCoffee(type);
            CoffeeMachine.InsertMoney(money);
            CoffeeMachine.SelectSize(Size.Normal);
            CoffeeMachine.SelectSugarLevel(SugarLevel.Low);
            CoffeeMachine.Run();
            CoffeeMachine.GetChange();
            CoffeeMachine.WorkStatus.Should().Be(WorkStatus.TakeYourCoffee);
        }

        [Test]
        public void ThreeTimesGetChange()
        {
            const int money = 189;
            for (var i = 0; i < 3; i++)
            {
                CoffeeMachine.InsertMoney(money);
                CoffeeMachine.GetChange().Should().Be(money);
            }
        }

        [Test]
        public void ChangeCoffeeTypeAfterCookng()
        {
            CoffeeMachine.InsertMoney(150);
            CoffeeMachine.SelectCoffee(CoffeeType.Espresso);
            CoffeeMachine.SelectSize(Size.Small);
            CoffeeMachine.SelectSugarLevel(SugarLevel.None);
            CoffeeMachine.Run();

            Assert.Throws<CoffeeMachineException>(() => CoffeeMachine.SelectCoffee(CoffeeType.Latte));
        }

        [Test]
        public void SelectUnknownCoffee()
        {
            CoffeeMachine.InsertMoney(40);
            Assert.Throws<CoffeeMachineException>(() => CoffeeMachine.SelectCoffee(CoffeeType.Unknown));
        }

        [Test]
        public void MakeCoffeeWithoutEnoughMoney_InsertMoneyFirst()
        {
            CoffeeMachine.InsertMoney(15);
            CoffeeMachine.SelectCoffee(CoffeeType.Espresso);
            CoffeeMachine.WorkStatus.Should().Be(WorkStatus.InsertMoneyOrSelectCoffee);
        }

        [Test]
        public void SelectSugarLevelWhenCoffeeIsDone()
        {
            CoffeeMachine.InsertMoney(100);
            CoffeeMachine.SelectCoffee(CoffeeType.Espresso);
            CoffeeMachine.SelectSugarLevel(SugarLevel.None);
            CoffeeMachine.SelectSize(Size.Normal);
            CoffeeMachine.Run();

            Assert.Throws<CoffeeMachineException>(() => CoffeeMachine.SelectSugarLevel(SugarLevel.Low));
        }

        [Test]
        public void SelectSugarLevelFewTimes()
        {
            CoffeeMachine.InsertMoney(150);
            CoffeeMachine.SelectCoffee(CoffeeType.Espresso);
            CoffeeMachine.SelectSugarLevel(SugarLevel.None);
            CoffeeMachine.SelectSugarLevel(SugarLevel.Medium);
            CoffeeMachine.SugarLevel.Should().Be(SugarLevel.Medium);
        }

        [Test]
        public void SelectSugarLevelHigh()
        {
            CoffeeMachine.InsertMoney(150);
            CoffeeMachine.SelectCoffee(CoffeeType.Espresso);
            CoffeeMachine.SelectSize(Size.Normal);
            CoffeeMachine.SelectSugarLevel(SugarLevel.High);
            CoffeeMachine.Size.Should().Be(Size.Normal);
        }

        [Test]
        public void SmallCoffeeCup()
        {
            CoffeeMachine.InsertMoney(150);
            CoffeeMachine.SelectCoffee(CoffeeType.Espresso);
            CoffeeMachine.SelectSize(Size.Small);
        }

        [Test]
        public void DoNotSelectSugarLevel()
        {
            CoffeeMachine.InsertMoney(150);
            CoffeeMachine.SelectCoffee(CoffeeType.Espresso);
            CoffeeMachine.SelectSize(Size.Large);
            CoffeeMachine.WorkStatus.Should().Be(WorkStatus.Customization);
        }

        [Test]
        public void ChangeCupSize()
        {
            CoffeeMachine.InsertMoney(100);
            CoffeeMachine.SelectCoffee(CoffeeType.Cappucchino);
            CoffeeMachine.SelectSize(Size.Normal);
            CoffeeMachine.SelectSize(Size.Small);
            CoffeeMachine.Size.Should().Be(Size.Small);
        }

        [Test]
        public void TwiceRun()
        {
            CoffeeMachine.InsertMoney(300);
            CoffeeMachine.SelectCoffee(CoffeeType.Latte);
            CoffeeMachine.SelectSize(Size.Normal);
            CoffeeMachine.SelectSugarLevel(SugarLevel.Low);
            CoffeeMachine.Run();
            Assert.Throws<CoffeeMachineException>(() => CoffeeMachine.Run());
        }

        [Test]
        public void HaveNoMoneyButWantCoffee()
        {
            CoffeeMachine.InsertMoney(69);
            CoffeeMachine.SelectCoffee(CoffeeType.Espresso);
            Assert.Throws<CoffeeMachineException>(() => CoffeeMachine.Run());
        }

        [TestCase(CoffeeType.Americano, SugarLevel.None, Size.Small)]
        [TestCase(CoffeeType.Americano, SugarLevel.None, Size.Normal)]
        [TestCase(CoffeeType.Americano, SugarLevel.None, Size.Large)]
        [TestCase(CoffeeType.Americano, SugarLevel.Low, Size.Small)]
        [TestCase(CoffeeType.Americano, SugarLevel.Low, Size.Normal)]
        [TestCase(CoffeeType.Americano, SugarLevel.Low, Size.Large)]
        [TestCase(CoffeeType.Americano, SugarLevel.Medium, Size.Small)]
        [TestCase(CoffeeType.Americano, SugarLevel.Medium, Size.Normal)]
        [TestCase(CoffeeType.Americano, SugarLevel.Medium, Size.Large)]
        [TestCase(CoffeeType.Americano, SugarLevel.High, Size.Small)]
        [TestCase(CoffeeType.Americano, SugarLevel.High, Size.Normal)]
        [TestCase(CoffeeType.Americano, SugarLevel.High, Size.Large)]
        [TestCase(CoffeeType.Espresso, SugarLevel.None, Size.Small)]
        [TestCase(CoffeeType.Espresso, SugarLevel.None, Size.Normal)]
        [TestCase(CoffeeType.Espresso, SugarLevel.None, Size.Large)]
        [TestCase(CoffeeType.Espresso, SugarLevel.Low, Size.Small)]
        [TestCase(CoffeeType.Espresso, SugarLevel.Low, Size.Normal)]
        [TestCase(CoffeeType.Espresso, SugarLevel.Low, Size.Large)]
        [TestCase(CoffeeType.Espresso, SugarLevel.Medium, Size.Small)]
        [TestCase(CoffeeType.Espresso, SugarLevel.Medium, Size.Normal)]
        [TestCase(CoffeeType.Espresso, SugarLevel.Medium, Size.Large)]
        [TestCase(CoffeeType.Espresso, SugarLevel.High, Size.Small)]
        [TestCase(CoffeeType.Espresso, SugarLevel.High, Size.Normal)]
        [TestCase(CoffeeType.Espresso, SugarLevel.High, Size.Large)]
        [TestCase(CoffeeType.Cappucchino, SugarLevel.None, Size.Small)]
        [TestCase(CoffeeType.Cappucchino, SugarLevel.None, Size.Normal)]
        [TestCase(CoffeeType.Cappucchino, SugarLevel.None, Size.Large)]
        [TestCase(CoffeeType.Cappucchino, SugarLevel.Low, Size.Small)]
        [TestCase(CoffeeType.Cappucchino, SugarLevel.Low, Size.Normal)]
        [TestCase(CoffeeType.Cappucchino, SugarLevel.Low, Size.Large)]
        [TestCase(CoffeeType.Cappucchino, SugarLevel.Medium, Size.Small)]
        [TestCase(CoffeeType.Cappucchino, SugarLevel.Medium, Size.Normal)]
        [TestCase(CoffeeType.Cappucchino, SugarLevel.Medium, Size.Large)]
        [TestCase(CoffeeType.Cappucchino, SugarLevel.High, Size.Small)]
        [TestCase(CoffeeType.Cappucchino, SugarLevel.High, Size.Normal)]
        [TestCase(CoffeeType.Cappucchino, SugarLevel.High, Size.Large)]
        [TestCase(CoffeeType.Latte, SugarLevel.None, Size.Small)]
        [TestCase(CoffeeType.Latte, SugarLevel.None, Size.Normal)]
        [TestCase(CoffeeType.Latte, SugarLevel.None, Size.Large)]
        [TestCase(CoffeeType.Latte, SugarLevel.Low, Size.Small)]
        [TestCase(CoffeeType.Latte, SugarLevel.Low, Size.Normal)]
        [TestCase(CoffeeType.Latte, SugarLevel.Low, Size.Large)]
        [TestCase(CoffeeType.Latte, SugarLevel.Medium, Size.Small)]
        [TestCase(CoffeeType.Latte, SugarLevel.Medium, Size.Normal)]
        [TestCase(CoffeeType.Latte, SugarLevel.Medium, Size.Large)]
        [TestCase(CoffeeType.Latte, SugarLevel.High, Size.Small)]
        [TestCase(CoffeeType.Latte, SugarLevel.High, Size.Normal)]
        [TestCase(CoffeeType.Latte, SugarLevel.High, Size.Large)]
        public void Test(CoffeeType coffeeType, SugarLevel sugarLevel, Size size)
        {
            var money = coffeeType.Price();
            CoffeeMachine.SelectCoffee(coffeeType);
            CoffeeMachine.InsertMoney(money);
            CoffeeMachine.WorkStatus.Should().Be(WorkStatus.Customization);

            CoffeeMachine.SelectSugarLevel(sugarLevel);
            CoffeeMachine.SelectSize(size);
            CoffeeMachine.WorkStatus.Should().Be(WorkStatus.ReadyForMakingCoffee);

            CoffeeMachine.Run();
            CoffeeMachine.WorkStatus.Should().Be(WorkStatus.TakeYourCoffee);

            CoffeeMachine.GetChange().Should().Be(0);

            var coffee = CoffeeMachine.EjectCup();
            coffee.Should().BeEquivalentTo(new Coffee(coffeeType, sugarLevel, size));
            CoffeeMachine.WorkStatus.Should().Be(WorkStatus.InsertMoneyOrSelectCoffee);
            CoffeeMachine.Size.Should().Be(Size.Unknown);
            CoffeeMachine.SugarLevel.Should().Be(SugarLevel.Unknown);
            CoffeeMachine.CoffeeType.Should().Be(CoffeeType.Unknown);
        }

        [Test]
        public void TestInsertTenCoins()
        {
            const CoffeeType coffeeType = CoffeeType.Cappucchino;
            CoffeeMachine.SelectCoffee(coffeeType);
            for (var i = 0; i < 10; i++)
            {
                CoffeeMachine.InsertMoney(1);
            }

            CoffeeMachine.Balance.Should().BeLessThan(coffeeType.Price());
            CoffeeMachine.WorkStatus.Should().Be(WorkStatus.InsertMoneyOrSelectCoffee);
        }

        [Test]
        public void TestInsertSameMoneyAmount()
        {
            const int moneyAmount = 10;
            CoffeeMachine.InsertMoney(moneyAmount);
            CoffeeMachine.InsertMoney(moneyAmount);
            CoffeeMachine.Balance.Should().Be(moneyAmount * 2);
        }

        [Test]
        public void TestSugarLevelAfterGettingChange()
        {
            const CoffeeType coffeeType = CoffeeType.Americano;
            CoffeeMachine.SelectCoffee(coffeeType);
            CoffeeMachine.InsertMoney(coffeeType.Price());
            CoffeeMachine.SelectSize(Size.Normal);
            CoffeeMachine.SelectSugarLevel(SugarLevel.Medium);
            CoffeeMachine.GetChange();
            CoffeeMachine.SugarLevel.Should().Be(SugarLevel.Unknown);
        }
    }
}
