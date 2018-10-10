using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Позволяет приготовить кофе, когда он уже готов")]
    public class Incorrect_RunB : CoffeeMachine
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
            Balance -= cost;
            WorkStatus = WorkStatus.TakeYourCoffee;
        }
    }
}