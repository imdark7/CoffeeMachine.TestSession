namespace CoffeeMachine
{
    public class CoffeeMachine : ICoffeeMachine
    {
        public int Balance { get; protected set; }
        public CoffeeType CoffeeType { get; protected set; }
        public SugarLevel SugarLevel { get; protected set; }
        public Size Size { get; protected set; }
        public WorkStatus WorkStatus { get; protected set; }

        public virtual void InsertMoney(int moneyAmount)
        {
            if (moneyAmount < 0)
            {
                throw new CoffeeMachineException("Amount must be a positive number");
            }
            if (WorkStatus == WorkStatus.TakeYourCoffee)
            {
                throw new CoffeeMachineException("You can't insert money. First, take a cup from the coffee machine.");
            }

            Balance += moneyAmount;
            WorkStatus = CoffeeType != CoffeeType.Unknown && Balance >= CoffeeType.Price()
                ? WorkStatus.Customization
                : WorkStatus.InsertMoneyOrSelectCoffee;
        }

        public virtual int GetChange()
        {
            var change = Balance;
            Balance = 0;
            if (WorkStatus != WorkStatus.TakeYourCoffee)
            {
                Reset();
            }
            return change;
        }

        public virtual void SelectCoffee(CoffeeType coffeeType)
        {
            if (WorkStatus == WorkStatus.TakeYourCoffee)
            {
                throw new CoffeeMachineException("You can't select coffee type. First, take a cup from the coffee machine");
            }
            if (coffeeType == CoffeeType.Unknown)
            {
                throw new CoffeeMachineException("You must select correct coffee type!");
            }
            CoffeeType = coffeeType;
            WorkStatus = Balance >= CoffeeType.Price() ? WorkStatus.Customization : WorkStatus.InsertMoneyOrSelectCoffee;
        }

        public virtual void SelectSugarLevel(SugarLevel sugarLevel)
        {
            if (WorkStatus == WorkStatus.InsertMoneyOrSelectCoffee)
            {
                throw new CoffeeMachineException("You can't select sugar level. First, select coffee type and insert money");
            }
            if (WorkStatus == WorkStatus.TakeYourCoffee)
            {
                throw new CoffeeMachineException("You can't select sugar level. First, take a cup from the coffee machine");
            }
            SugarLevel = sugarLevel;
            if (SugarLevel != SugarLevel.Unknown && Size != Size.Unknown)
            {
                WorkStatus = WorkStatus.ReadyForMakingCoffee;
            }
        }

        public virtual void SelectSize(Size size)
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
            if (SugarLevel != SugarLevel.Unknown && Size != Size.Unknown)
            {
                WorkStatus = WorkStatus.ReadyForMakingCoffee;
            }
        }

        public virtual void Run()
        {
            if (WorkStatus == WorkStatus.TakeYourCoffee)
            {
                throw new CoffeeMachineException("You can't start making coffee. First, take a cup from the coffee machine");
            }
            if (WorkStatus != WorkStatus.ReadyForMakingCoffee)
            {
                throw new CoffeeMachineException("You can't start making coffee. First, select Coffee Type, Size and Sugar level");
            }
            Balance -= CoffeeType.Price();
            WorkStatus = WorkStatus.TakeYourCoffee;
        }

        public virtual Coffee EjectCup()
        {
            if (WorkStatus != WorkStatus.TakeYourCoffee)
            {
                throw new CoffeeMachineException("You can't eject cup. Coffee is not ready yet.");
            }

            var coffee = new Coffee(CoffeeType, SugarLevel, Size);
            Reset();
            return coffee;
        }

        protected virtual void Reset()
        {
            WorkStatus = WorkStatus.InsertMoneyOrSelectCoffee;
            CoffeeType = CoffeeType.Unknown;
            SugarLevel = SugarLevel.Unknown;
            Size = Size.Unknown;
        }
    }
}