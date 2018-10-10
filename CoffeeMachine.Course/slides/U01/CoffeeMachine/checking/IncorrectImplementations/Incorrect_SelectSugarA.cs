using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Можно перевыбрать количество сахара, когда кофе уже готов")]
    public class Incorrect_SelectSugarA : CoffeeMachine
    {
        public override void SelectSugarLevel(SugarLevel sugarLevel)
        {
            if (WorkStatus == WorkStatus.InsertMoneyOrSelectCoffee)
            {
                throw new CoffeeMachineException("You can't select sugar level. First, select coffee type and insert money");
            }
            SugarLevel = sugarLevel;
            if (SugarLevel != SugarLevel.Unknown && Size != Size.Unknown)
            {
                WorkStatus = WorkStatus.ReadyForMakingCoffee;
            }
        }
    }
}