using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Нельзя перевыбрать уровень сахара")]
    public class Incorrect_SelectSugarB : CoffeeMachine
    {
        public override void SelectSugarLevel(SugarLevel sugarLevel)
        {
            if (WorkStatus == WorkStatus.InsertMoneyOrSelectCoffee)
            {
                throw new CoffeeMachineException("You can't select sugar level. First, select coffee type and insert money");
            }
            if (WorkStatus == WorkStatus.TakeYourCoffee)
            {
                throw new CoffeeMachineException("You can't select sugar level. First, take a cup from the coffee machine");
            }
            if (SugarLevel == SugarLevel.Unknown)
            {
                SugarLevel = sugarLevel;
            }
            if (SugarLevel != SugarLevel.Unknown && Size != Size.Unknown)
            {
                WorkStatus = WorkStatus.ReadyForMakingCoffee;
            }
        }
    }
}