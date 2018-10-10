using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Можно приготовить кофе не выбрав уровень сахара")]
    public class Incorrect_SelectSizeB : CoffeeMachine
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
            Size = size;
            if (Size != Size.Unknown)
            {
                WorkStatus = WorkStatus.ReadyForMakingCoffee;
            }
        }
    }
}