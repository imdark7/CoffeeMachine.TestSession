using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Можно перевыбрать кофе после его приготовления")]
    public class Incorrect_SelectCoffeeA : CoffeeMachine
    {
        public override void SelectCoffee(CoffeeType coffeeType)
        {
            if (coffeeType == CoffeeType.Unknown)
            {
                throw new CoffeeMachineException("You must select correct coffee type!");
            }
            CoffeeType = coffeeType;
            WorkStatus = Balance >= CoffeeType.Price() ? WorkStatus.Customization : WorkStatus.InsertMoneyOrSelectCoffee;
        }
    }
}