using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day23
    {
        public string Solve(string input, int rounds)
        {
            var data = input.ToCharArray().Select(c => int.Parse(c.ToString())).ToList();
            var maxValue = data.Max();

            var finalState = SolveInner(data, rounds, maxValue);

            var pos1 = finalState.IndexOf(1);
            var after1 = finalState.Skip(pos1 + 1);
            var wrapped = finalState.Take(pos1);
            var all = after1.Union(wrapped);
            var result = string.Join("", all);
            return result;
        }

        public long Solve2(string input)
        {
            const int maxValue = 1000000;
            var data = input.ToCharArray().Select(c => int.Parse(c.ToString())).ToList();
            for (var i = data.Count + 1; i <= maxValue; i++)
            {
                data.Add(i);
            }

            var finalState = SolveInner(data, 10000000, maxValue);

            var pos1 = finalState.IndexOf(1);
            var firstStar = finalState[pos1 + 1];
            var secondStar = finalState[pos1 + 2];
            var result = (long)firstStar * secondStar;
            return result;
        }

        private static List<int> SolveInner(List<int> data, int rounds, int maxValue)
        {
            var dict = new Dictionary<int, int>();

            for (var index = 0; index < data.Count - 1; index++)
            {
                dict[data[index]] = data[index + 1];
            }

            dict[data[^1]] = data[0];

            var currentVal = data[0];
            for (var i = 0; i < rounds; i++)
            {
                var removed = new List<int>();
                for (var j = 0; j < 3; j++)
                {
                    var r = dict[currentVal];
                    removed.Add(r);
                    dict[currentVal] = dict[r];
                    dict.Remove(r);
                }

                var destVal = currentVal - 1;
                if (destVal == 0)
                {
                    destVal = maxValue;
                }

                while (removed.Contains(destVal))
                {
                    destVal--;
                    if (destVal == 0)
                    {
                        destVal = maxValue;
                    }
                }

                for (var index = 1; index <= removed.Count; index++)
                {
                    var r = removed[^index];
                    var currentNext = dict[destVal];
                    dict[destVal] = r;
                    dict[r] = currentNext;
                }

                currentVal = dict[currentVal];
            }

            var results = new List<int>();
            var current = 1;
            for(var i = 0; i < maxValue; i++)
            {
                results.Add(current);
                current = dict[current];
            }

            return results;
        }
    }
}