using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Не сбрасывается Уровень сахара после того, вернули сдачу")]
    [Secret]
    public class Incorrect_GetChangeD : CoffeeMachine
    {
        public override int GetChange()
        {
            var change = Balance;
            Balance = 0;
            if (WorkStatus != WorkStatus.TakeYourCoffee)
            {
                WorkStatus = WorkStatus.InsertMoneyOrSelectCoffee;
                CoffeeType = CoffeeType.Unknown;
                Size = Size.Unknown;
            }
            return change;
        }
    }
}