using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Можно закинуть деньги только после выбора кофе")]
    public class Incorrect_InsertMoneyC : CoffeeMachine
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
            if (CoffeeType == CoffeeType.Unknown)
            {
                throw new CoffeeMachineException("You can't insert money. First, select a coffee type.");
            }

            Balance += moneyAmount;
            WorkStatus =
                Balance >= CoffeeType.Price()
                ? WorkStatus.Customization
                : WorkStatus.InsertMoneyOrSelectCoffee;
        }
    }
}