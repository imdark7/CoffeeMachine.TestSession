using System;

namespace CoffeeMachine
{
    internal class CoffeeMachineException : Exception
    {
        public CoffeeMachineException(string message) : base(message)
        {
        }
    }
}