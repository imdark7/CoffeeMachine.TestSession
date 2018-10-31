using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Можно ввести отрицательное значение денег, если баланс не уйдет ниже 0")]
    [Secret]
    public class Incorrect_InsertMoneyB : CoffeeMachine
    {
        public override void InsertMoney(int moneyAmount)
        {
            if (moneyAmount + Balance < 0)
            {
                throw new CoffeeMachineException("Amount must be a positive number");
            }
            if (WorkStatus == WorkStatus.TakeYourCoffee)
            {
                throw new CoffeeMachineException("You can't insert money. First, take a cup from the coffee machine.");
            }

            Balance += moneyAmount;
            WorkStatus = CoffeeType != CoffeeType.Unknown && Balance >= CoffeeType.Price()
                ? WorkStatus.Customization
                : WorkStatus.InsertMoneyOrSelectCoffee;
        }
    }
}