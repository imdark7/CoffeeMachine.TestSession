using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeMachine.Mechanics
{
    public static class EnumerableExtensions
    {
        public static string SerializeNoMoreThan(this IEnumerable<string> collection, int limit, string ending = null)
        {
            var result = "";
            var array = collection.ToArray();
            if (limit < 1)
                throw new ArgumentOutOfRangeException(nameof(limit));
            for (var i = 0; i < array.Length; i++)
            {
                if (i < limit)
                {
                    result += $"{array[i]}, ";
                }
                else
                {
                    result = result.TrimEnd(' ').TrimEnd(',');
                    result += $" и еще {array.Length - limit}";
                    if (ending != null)
                        result += " " + ending;
                    break;
                }
            }

            return result.TrimEnd(' ').TrimEnd(',').EnsureFullStopOnEnd();
        }
    }
}