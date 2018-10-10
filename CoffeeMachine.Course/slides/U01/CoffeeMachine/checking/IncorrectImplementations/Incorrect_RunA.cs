using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Всегда рассчитывает стоимость как за латте")]
    public class Incorrect_RunA : CoffeeMachine
    {
        public override void Run()
        {
            if (CoffeeType == CoffeeType.Unknown)
            {
                throw new CoffeeMachineException("You can't start making coffee. First, select coffee type");
            }

            if (WorkStatus != WorkStatus.ReadyForMakingCoffee)
            {
                throw new CoffeeMachineException("You can't start making coffee. First, select Size and Sugar level");
            }

            var cost = CoffeeType.Latte.Price();
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