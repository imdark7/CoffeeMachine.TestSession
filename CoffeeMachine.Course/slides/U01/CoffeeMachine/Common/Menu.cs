using System;

namespace CoffeeMachine
{
    public static class Menu

    {
        public static int Price(this CoffeeType coffeeType)
        {
            switch (coffeeType)
            {
                case CoffeeType.Espresso:
                    return 50;
                case CoffeeType.Americano:
                    return 60;
                case CoffeeType.Cappucchino:
                    return 100;
                case CoffeeType.Latte:
                    return 110;
                default:
                    throw new ArgumentOutOfRangeException(nameof(coffeeType), coffeeType, null);
            }
        }
    }
}