using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day18Part2
    {
        public long Solve(List<string> input)
        {
            return input.Select(Solve).Sum();
        }

        public long Solve(string input)
        {
            var cleaned = input.Replace(" ", "");
            return SolveMultiplication(cleaned);
        }

        private long SolveMultiplication(string input)
        {
            var nums = Split(input, '*');
            var values = nums.Select(SolveAddition);
            var result = values.Aggregate(1L, (a, b) => a * b, i => i);
            return result;
        }

        private long SolveAddition(string input)
        {
            var parts = Split(input, '+');

            var nums = parts.Select(num =>
                num.StartsWith("(")
                ? SolveMultiplication(num.Substring(1, num.Length - 2))
                : long.Parse(num)).ToList();
            var result = nums.Aggregate(0L, (a, b) => a + b, i => i);
            return result;
        }

        private static IEnumerable<string> Split(string input, char separator)
        {
            var result = new List<string>();

            var parens = 0;
            var chunk = "";
            foreach (var c in input)
            {
                if (c == '(') {
                    parens++;
                }
                else if (c == ')') {
                    parens--;
                }

                if (parens == 0 && separator == c)
                {
                    result.Add(chunk);
                    chunk = "";
                }
                else
                {
                    chunk += c;
                }
            }
            if (chunk != "") {
                result.Add(chunk);
            }
            return result;
        }
    }
}