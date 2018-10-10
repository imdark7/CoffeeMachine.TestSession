using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Нельзя выбрать маленький размер чашки")]
    public class Incorrect_SelectSizeA : CoffeeMachine
    {
        public override void SelectSize(Size size)
        {
            if (WorkStatus == WorkStatus.InsertMoneyOrSelectCoffee)
            {
                throw new CoffeeMachineException("You can't select cup size. First, select coffee type and insert money");
            }
            if (WorkStatus == WorkStatus.TakeYourCoffee)
            {
                throw new CoffeeMachineException("You can't select cup size. First, take a cup from the coffee machine");
            }
            if (size == Size.Small)
            {
                throw new CoffeeMachineException("You can't select small size. Small cups are over");
            }
            Size = size;
            if (SugarLevel != SugarLevel.Unknown && Size != Size.Unknown)
            {
                WorkStatus = WorkStatus.ReadyForMakingCoffee;
            }
        }
    }
}