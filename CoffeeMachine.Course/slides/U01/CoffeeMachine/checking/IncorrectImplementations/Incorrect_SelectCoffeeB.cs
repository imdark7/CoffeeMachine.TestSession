using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Можно выбрать тип кофе Unknown")]
    public class Incorrect_SelectCoffeeB : CoffeeMachine
    {
        public override void SelectCoffee(CoffeeType coffeeType)
        {
            if (WorkStatus == WorkStatus.TakeYourCoffee)
            {
                throw new CoffeeMachineException("You can't select coffee type. First, take a cup from the coffee machine");
            }
            CoffeeType = coffeeType;
            WorkStatus = Balance >= CoffeeType.Price() ? WorkStatus.Customization : WorkStatus.InsertMoneyOrSelectCoffee;
        }
    }
}