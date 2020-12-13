using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day13
    {
        public int Solve(List<string> input)
        {
            var currentTime = int.Parse(input[0]);
            var buses = input[1].Split(',').Where(b => b != "x").Select(int.Parse);

            var nearestTimes = buses.ToDictionary(b => b, b => NextMultiple(b, currentTime));

            var nextTime = nearestTimes.Values.Min();
            var (bus, time) = nearestTimes.Single(b => b.Value == nextTime);
            return bus * (time - currentTime);
        }

        private static int NextMultiple(int i, int limit)
        {
            return ((limit / i) + 1) * i;
        }
    }
}