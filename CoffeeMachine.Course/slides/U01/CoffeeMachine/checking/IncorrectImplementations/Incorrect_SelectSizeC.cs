﻿using CoffeeMachine.Mechanics;

namespace CoffeeMachine.IncorrectImplementations
{
    [IncorrectImplementation("Можно перевыбрать размер чашки только на большую")]
    public class Incorrect_SelectSizeC : CoffeeMachine
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
            if (Size < size)
            {
                Size = size;
            }
            if (SugarLevel != SugarLevel.Unknown && Size != Size.Unknown)
            {
                WorkStatus = WorkStatus.ReadyForMakingCoffee;
            }
        }
    }
}