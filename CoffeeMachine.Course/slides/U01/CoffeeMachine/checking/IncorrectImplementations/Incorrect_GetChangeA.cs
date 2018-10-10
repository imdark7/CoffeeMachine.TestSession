using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Баланс не меняется, если забирать сдачу при статусе TakeYourCoffee")]
    public class Incorrect_GetChangeA : CoffeeMachine
    {
        public override int GetChange()
        {
            var change = Balance;
            if (WorkStatus != WorkStatus.TakeYourCoffee)
            {
                Balance = 0;
                Reset();
            }
            return change;
        }
    }
}