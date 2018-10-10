using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Можно перейти к кастомизации кофе, даже если не хватает денег")]
    public class Incorrect_InsertMoneyE : CoffeeMachine
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

            Balance += moneyAmount;
            WorkStatus = CoffeeType != CoffeeType.Unknown
                ? WorkStatus.Customization
                : WorkStatus.InsertMoneyOrSelectCoffee;
        }
    }
}