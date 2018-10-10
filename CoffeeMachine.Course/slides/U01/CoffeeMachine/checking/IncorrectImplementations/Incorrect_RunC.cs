using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Можно сделать кофе, если не выбраны Size и/или SugarLevel")]
    public class Incorrect_RunC : CoffeeMachine
    {
        public override void Run()
        {
            if (CoffeeType == CoffeeType.Unknown)
            {
                throw new CoffeeMachineException("You can't start making coffee. First, select coffee type");
            }

            var cost = CoffeeType.Price();
            if (cost > Balance)
            {
                throw new CoffeeMachineException("You can't start making coffee. Not enough money for selected coffee type");
            }

            if (WorkStatus == WorkStatus.TakeYourCoffee)
            {
                throw new CoffeeMachineException("You can't start making coffee. First, take a cup from the coffee machine");
            }
            Balance -= cost;
            WorkStatus = WorkStatus.TakeYourCoffee;
        }
    }
}