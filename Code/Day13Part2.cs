using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day13Part2
    {
        public long Solve(List<string> input)
        {
            var buses = Parse(input[1]);

            long total = 0;
            long step = 1;

            for (var i = 0; i < buses.Count; i++)
            {
                while (!Matches(buses[i], total))
                {
                    total += step;
                }

                step *= buses[i].Item2;
            }

            return total;
        }

        private static bool Matches(Tuple<long, long> bus, long b)
        {
            return (b + bus.Item1) % bus.Item2 == 0;
        }

        private List<Tuple<long, long>> Parse(string s)
        {
            var parts = s.Split(',');
            var result = new List<Tuple<long, long>>();

            for (var i = 0; i < parts.Length; i++)
            {
                if (int.TryParse(parts[i], out var n))
                {
                    result.Add(new Tuple<long, long>(i, n));
                }
            }

            return result;
        }
    }
}