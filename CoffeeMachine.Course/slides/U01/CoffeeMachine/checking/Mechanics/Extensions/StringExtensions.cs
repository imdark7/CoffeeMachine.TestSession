using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeMachine.Mechanics
{
    public static class StringExtentions
    {
        public static string EnsureFullStopOnEnd(this string str)
        {
            var last = str.Last();
            if (last != '.' && last != '!')
            {
                return str + '.';
            }

            return str;
        }


        public static string TrimWhitespaces(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            var whitespaceCounter = 0;
            var excessWhitespacesIndices = new List<int>();
            str = str.TrimStart(' ').TrimEnd(' ');
            var sb = new StringBuilder(str);
            for (var i = 0; i < str.Length; i++)
            {
                if (sb[i] == ' ')
                {
                    whitespaceCounter++;
                    if (whitespaceCounter > 1)
                    {
                        excessWhitespacesIndices.Add(i);
                    }
                }
                else
                {
                    whitespaceCounter = 0;
                }
            }

            var sb2 = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                if (!excessWhitespacesIndices.Contains(i)) sb2.Append(str[i]);
            }

            return sb2.ToString();
        }
    }
}