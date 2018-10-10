using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Если вводить деньги по одному рублю, то можно приготовить любой кофе всего за 10 рублей")]
    [Secret]
    public class Incorrect_InsertMoneyF : CoffeeMachine
    {
        private int _coinCounter;
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
            if (moneyAmount == 1)
            {
                _coinCounter++;
            }
            else
            {
                _coinCounter = 0;
            }
            WorkStatus = CoffeeType != CoffeeType.Unknown && (Balance >= CoffeeType.Price() || _coinCounter == 10)
                ? WorkStatus.Customization
                : WorkStatus.InsertMoneyOrSelectCoffee;
        }
    }
}
