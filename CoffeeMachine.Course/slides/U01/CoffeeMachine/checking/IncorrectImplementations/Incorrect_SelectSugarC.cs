using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Если выбрать количество сахара High, то размер чашки меняется на Large")]
    [Secret]
    public class Incorrect_SelectSugarC : CoffeeMachine
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
            if ((SugarLevel = sugarLevel) == SugarLevel.High)
            {
                Size = Size.Large;
            }
            if (SugarLevel != SugarLevel.Unknown && Size != Size.Unknown)
            {
                WorkStatus = WorkStatus.ReadyForMakingCoffee;
            }
        }
    }
}