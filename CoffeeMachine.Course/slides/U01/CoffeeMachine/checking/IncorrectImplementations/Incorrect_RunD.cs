using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Готовит всегда Латте")]
    public class Incorrect_RunD : CoffeeMachine
    {
        public override void Run()
        {
            base.Run();

            CoffeeType = CoffeeType.Latte;
        }
    }
}