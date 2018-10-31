using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Баланс не меняется, если забирать сдачу при статусе Customization")]
    public class Incorrect_GetChangeA : CoffeeMachine
    {
        public override int GetChange()
        {
            var change = Balance;
            if (WorkStatus != WorkStatus.Customization)
            {
                Balance = 0;
            }
            if (WorkStatus != WorkStatus.TakeYourCoffee)
            {
                Reset();
            }
            return change;
        }
    }
}