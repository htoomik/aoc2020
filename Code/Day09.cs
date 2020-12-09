using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day09
    {
        public long Solve(List<string> input, int windowSize)
        {
            var numbers = input.Select(long.Parse).ToList();
            var window = new List<long>();
            window.AddRange(numbers.Take(windowSize));

            for (var i = windowSize; i < input.Count; i++)
            {
                var current = numbers[i];
                if (!Check(current, window))
                {
                    return current;
                }

                window.RemoveAt(0);
                window.Add(current);
            }

            throw new Exception();
        }

        public long Solve2(List<string> input, long target)
        {
            var numbers = input.Select(long.Parse).ToList();

            for (var start = 0; start < numbers.Count(); start++)
            {
                long sum = 0;
                var all = new List<long>();
                for (var i = start; i < numbers.Count(); i++)
                {
                    sum += numbers[i];
                    all.Add(numbers[i]);
                    if (sum == target)
                    {
                        var min = all.Min();
                        var max = all.Max();
                        return min + max;
                    }
                    if (sum > target)
                    {
                        break;
                    }
                }
            }

            throw new Exception();
        }

        private static bool Check(long target, List<long> numbers)
        {
            for (var i = 0; i < numbers.Count; i++)
            {
                for (var j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[i] + numbers[j] == target)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}