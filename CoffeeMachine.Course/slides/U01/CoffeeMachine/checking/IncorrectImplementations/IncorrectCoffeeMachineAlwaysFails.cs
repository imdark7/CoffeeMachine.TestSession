using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Падает при вызове любого метода")]
    public class IncorrectCoffeeMachineAlwaysFails : CoffeeMachine
    {
        public override void InsertMoney(int moneyAmount)
        {
            throw new CoffeeMachineException("Unexpected error");
        }

        public override int GetChange()
        {
            throw new CoffeeMachineException("Unexpected error");
        }

        public override void SelectCoffee(CoffeeType coffeeType)
        {
            throw new CoffeeMachineException("Unexpected error");
        }

        public override void SelectSize(Size size)
        {
            throw new CoffeeMachineException("Unexpected error");
        }

        public override void SelectSugarLevel(SugarLevel sugarLevel)
        {
            throw new CoffeeMachineException("Unexpected error");
        }

        public override void Run()
        {
            throw new CoffeeMachineException("Unexpected error");
        }

        public override Coffee EjectCup()
        {
            throw new CoffeeMachineException("Unexpected error");
        }
    }
}
