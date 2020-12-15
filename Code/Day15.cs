using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day15
    {
        public int Solve(string input, int target)
        {
            var parsed = input.Split(",").Select(int.Parse).ToList();
            var nums = new List<int>();
            var positions = new Dictionary<int, int>();

            // Add all but last one
            for (var i = 0; i < parsed.Count - 1; i++)
            {
                var n = parsed[i];
                nums.Add(n);
                positions[n] = i;
            }

            var current = parsed.Last();
            var newNum = 0;

            for (var i = parsed.Count - 1; i < target; i++)
            {
                if (positions.ContainsKey(current))
                {
                    var pos = positions[current];
                    newNum = i - pos;
                }
                else
                {
                    newNum = 0;
                }
                nums.Add(current);
                positions[current] = i;
                current = newNum;
            }

            return nums.Last();
        }
    }
}