using System.Collections.Generic;

namespace aoc2020.Code
{
    public class Day03
    {
        public int Solve(List<string> input, int slopeX, int slopeY)
        {
            var x = 0;
            var count = 0;

            for (var y = 0; y < input.Count; y += slopeY)
            {
                var line = input[y];
                var isTree = line[x] == '#';

                if (isTree)
                {
                    count++;
                }

                x += slopeX;
                x %= input[0].Length;
            }

            return count;
        }

        public long Solve2(List<string> map)
        {
            var r1 = Solve(map, 1, 1);
            var r2 = Solve(map, 3, 1);
            var r3 = Solve(map, 5, 1);
            var r4 = Solve(map, 7, 1);
            var r5 = Solve(map, 1, 2);

            var result = (long)r1 * r2 * r3 * r4 * r5;
            return result;
        }
    }
}