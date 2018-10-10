using System.Collections.Generic;
using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Баланс пополняется, только если вводить разные суммы")]
    [Secret]
    public class Incorrect_InsertMoneyG : CoffeeMachine
    {
        private readonly List<int> _cashIn = new List<int>();
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

            if (!_cashIn.Contains(moneyAmount))
            {
                Balance += moneyAmount;
                _cashIn.Add(moneyAmount);
            }
            WorkStatus = CoffeeType != CoffeeType.Unknown && Balance >= CoffeeType.Price()
                ? WorkStatus.Customization
                : WorkStatus.InsertMoneyOrSelectCoffee;
        }
    }
}