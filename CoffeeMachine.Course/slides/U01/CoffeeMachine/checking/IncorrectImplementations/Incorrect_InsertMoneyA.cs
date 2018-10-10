using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Можно закинуть деньги, пока не достал готовый кофе")]
    public class Incorrect_InsertMoneyA : CoffeeMachine
    {
        public override void InsertMoney(int moneyAmount)
        {
            if (moneyAmount < 0)
            {
                throw new CoffeeMachineException("Amount must be a positive number");
            }
            Balance += moneyAmount;
            WorkStatus = CoffeeType != CoffeeType.Unknown && Balance >= CoffeeType.Price()
                ? WorkStatus.Customization
                : WorkStatus.InsertMoneyOrSelectCoffee;
        }
    }
}