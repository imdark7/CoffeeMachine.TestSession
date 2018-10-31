using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Позволяет приготовить кофе, когда он уже готов")]
    public class Incorrect_RunB : CoffeeMachine
    {
        public override void Run()
        {
            if (WorkStatus != WorkStatus.ReadyForMakingCoffee)
            {
                throw new CoffeeMachineException("You can't start making coffee. First, select Coffee Type, Size and Sugar level");
            }
            Balance -= CoffeeType.Price();
            WorkStatus = WorkStatus.TakeYourCoffee;
        }
    }
}