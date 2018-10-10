using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Можно приготовить кофе, если не хватает денег на него")]
    public class Incorrect_SelectCoffeeC : CoffeeMachine
    {
        public override void SelectCoffee(CoffeeType coffeeType)
        {
            if (WorkStatus == WorkStatus.TakeYourCoffee)
            {
                throw new CoffeeMachineException("You can't select coffee type. First, take a cup from the coffee machine");
            }
            if (coffeeType == CoffeeType.Unknown)
            {
                throw new CoffeeMachineException("You must select correct coffee type!");
            }
            CoffeeType = coffeeType;
            WorkStatus = WorkStatus.Customization;
        }
    }
}