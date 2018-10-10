using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Если три раза вернуть ненулевую сдачу, то на третий раз вернется 1000")]
    [Secret]
    public class Incorrect_GetChangeC : CoffeeMachine
    {
        private int count;
        public override int GetChange()
        {
            var change = Balance;
            Balance = 0;
            if (WorkStatus != WorkStatus.TakeYourCoffee)
            {
                Reset();
            }
            if (change > 0)
            {
                count++;
            }
            return count == 3 ? 1000 : change;
        }
    }
}
