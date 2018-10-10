using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Если кофе выбран, при вводе денег к балансу добавляется стоимость кофе, а не введеннная сумма")]
    [Secret]
    public class Incorrect_InsertMoneyD : CoffeeMachine
    {
        public override void InsertMoney(int moneyAmount)
        {
            if (moneyAmount < 0)
            {
                throw new CoffeeMachineException("Amount must be a positive number");
            }
            if (WorkStatus == WorkStatus.TakeYourCoffee)
            {
                throw new CoffeeMachineException("You can't insert money. First, take a cup from the coffee machine.");
            }

            Balance += CoffeeType != CoffeeType.Unknown ? CoffeeType.Price() : moneyAmount;

            WorkStatus = CoffeeType != CoffeeType.Unknown && Balance >= CoffeeType.Price()
                ? WorkStatus.Customization
                : WorkStatus.InsertMoneyOrSelectCoffee;
        }
    }
}