using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day06
    {
        public int Solve(string input)
        {
            var groups = input.Trim().Replace("\r", "").Split("\n\n");
            return groups.Select(g => g.Replace("\n", "").ToCharArray().Distinct().Count()).Sum();
        }

        public int Solve2(string input)
        {
            var groups = input.Trim().Replace("\r\n", "\n").Split("\n\n");
            var counts = groups.Select(CountIntersections).Sum();
            return counts;
        }

        private int CountIntersections(string arg)
        {
            var individuals = arg.Split("\n");
            IEnumerable<char> seed = individuals[0].ToCharArray();
            var intersections = individuals.Aggregate(seed, (list, s) => list.Intersect(s.ToCharArray()));
            return intersections.Count();
        }
    }
}