using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Кофе можно забрать, когда он еще не готов")]
    public class Incorrect_EjectCupB : CoffeeMachine
    {
        public override Coffee EjectCup()
        {
            var coffee = new Coffee(CoffeeType, SugarLevel, Size);
            Reset();
            return coffee;
        }
    }
}