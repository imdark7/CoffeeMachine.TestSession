using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Кофе можно забрать, когда его еще не приготовили")]
    public class Incorrect_EjectCupB : CoffeeMachine
    {
        public override Coffee EjectCup()
        {
            if (WorkStatus == WorkStatus.Customization || WorkStatus == WorkStatus.InsertMoneyOrSelectCoffee)
            {
                throw new CoffeeMachineException("You can't eject cup. Coffee is not ready yet.");
            }
            var coffee = new Coffee(CoffeeType, SugarLevel, Size);
            Reset();
            return coffee;
        }
    }
}