using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Size не сбрасывается после приготовления")]
    public class Incorrect_EjectCupA : CoffeeMachine
    {
        public override Coffee EjectCup()
        {
            if (WorkStatus != WorkStatus.TakeYourCoffee)
            {
                throw new CoffeeMachineException("You can't eject cup. Coffee is not ready yet.");
            }

            var coffee = new Coffee(CoffeeType, SugarLevel, Size);

            WorkStatus = WorkStatus.InsertMoneyOrSelectCoffee;
            CoffeeType = CoffeeType.Unknown;
            SugarLevel = SugarLevel.Unknown;

            return coffee;
        }
    }
}