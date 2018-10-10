using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Сбрасывается статус работы, если забрать сдачу прежде чем забрали готовый кофе")]
    public class Incorrect_GetChangeB : CoffeeMachine
    {
        public override int GetChange()
        {
            var change = Balance;
            Balance = 0;
            Reset();
            return change;
        }
    }
}