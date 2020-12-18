using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day18
    {
        public long Solve(List<string> input)
        {
            return input.Select(s => Solve(s)).Sum();
        }

        public long Solve(string input)
        {
            var cleaned = input.Replace(" ", "");
            var (result, _) = Solve(cleaned, 0);
            return result;
        }

        private (long result, int newI) Solve(string input, int i)
        {
            long result = 0;
            var op = '+';

            while (i < input.Length)
            {
                var c = input[i];
                if (c == '+' || c == '*')
                {
                    op = c;
                    i++;
                    continue;
                }

                if (c == ')')
                {
                    return (result, i + 1);
                }

                if (long.TryParse(c.ToString(), out var num))
                {
                    i++;
                }
                else if (c == '(')
                {
                    (num, i) = Solve(input, i + 1);
                }

                switch (op)
                {
                    case '+':
                        result += num;
                        break;
                    case '*':
                        result *= num;
                        break;
                }
            }

            return (result, i);
        }
    }
}