using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Можно сделать кофе, если не выбраны Size и/или SugarLevel")]
    public class Incorrect_RunC : CoffeeMachine
    {
        public override void Run()
        {
            if (WorkStatus == WorkStatus.TakeYourCoffee)
            {
                throw new CoffeeMachineException("You can't start making coffee. First, take a cup from the coffee machine");
            }
            if (WorkStatus == WorkStatus.InsertMoneyOrSelectCoffee)
            {
                throw new CoffeeMachineException("You can't start making coffee. First, select Coffee Type, Size and Sugar level");
            }
            Balance -= CoffeeType.Price();
            WorkStatus = WorkStatus.TakeYourCoffee;
        }
    }
}