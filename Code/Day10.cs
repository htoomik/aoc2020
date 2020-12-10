using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day10
    {
        public int Solve(List<string> input)
        {
            var nums = GetFullList(input);

            var ones = 0;
            var threes = 0;

            var current = 0;
            foreach (var num in nums)
            {
                var diff = num - current;
                if (diff == 1)
                {
                    ones++;
                }

                if (diff == 3)
                {
                    threes++;
                }

                current = num;
            }

            return threes * ones;
        }

        public long Solve2(List<string> input)
        {
            var nums = GetFullList(input);

            long total = 1;
            var consecutiveOnes = 0;

            var current = 0;
            foreach (var num in nums)
            {
                var diff = num - current;
                if (diff == 1)
                {
                    consecutiveOnes++;
                }

                if (diff == 3)
                {
                    switch (consecutiveOnes)
                    {
                        case 2:
                            total *= 2;
                            break;
                        case 3:
                            total *= 4;
                            break;
                        case 4:
                            total *= 7;
                            break;
                    }

                    consecutiveOnes = 0;
                }

                current = num;
            }

            return total;
        }

        private static List<int> GetFullList(List<string> input)
        {
            var nums = input.Select(int.Parse);
            var ordered = nums.OrderBy(n => n).ToList();
            ordered.Add(ordered.Max() + 3);
            return ordered;
        }
    }
}