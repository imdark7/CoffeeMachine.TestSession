namespace CoffeeMachine
{
    public class Coffee
    {
        public Coffee(CoffeeType coffeeType, SugarLevel sugarLevel, Size size)
        {
            Type = coffeeType;
            Sugar = sugarLevel;
            Size = size;
        }

        public readonly CoffeeType Type;
        public readonly SugarLevel Sugar;
        public readonly Size Size;
    }
}