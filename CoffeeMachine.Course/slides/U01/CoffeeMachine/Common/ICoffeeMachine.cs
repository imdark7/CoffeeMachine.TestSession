namespace CoffeeMachine
{
    public interface ICoffeeMachine
    {
        int Balance { get; }
        CoffeeType CoffeeType { get; }
        SugarLevel SugarLevel { get; }
        Size Size { get; }
        WorkStatus WorkStatus { get; }

        void InsertMoney(int moneyAmount);
        int GetChange();
        void SelectCoffee(CoffeeType coffeeType);
        void SelectSugarLevel(SugarLevel sugarLevel);
        void SelectSize(Size size);
        void Run();
        Coffee EjectCup();
    }
}