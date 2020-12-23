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
            var current = 0;
            var currentVal = data[0];
            for (var i = 0; i < rounds; i++)
            {
                var removed = new List<int>();
                var pos = current + 1;
                for (var j = 0; j < 3; j++)
                {
                    if (pos == data.Count)
                    {
                        pos = 0;
                    }

                    var r = data[pos];
                    data.RemoveAt(pos);
                    removed.Add(r);
                }

                var destination = currentVal - 1;
                if (destination == 0)
                {
                    destination = maxValue;
                }
                while (removed.Contains(destination))
                {
                    destination--;
                    if (destination == 0)
                    {
                        destination = maxValue;
                    }
                }

                var destPos = data.IndexOf(destination);
                data.InsertRange(destPos + 1, removed);

                while (data[current] != currentVal)
                {
                    var first = data[0];
                    data.RemoveAt(0);
                    data.Add(first);
                }

                current = data.IndexOf(currentVal) + 1;
                if (current == data.Count())
                {
                    current = 0;
                }

                currentVal = data[current];
            }

            var pos1 = data.IndexOf(1);
            var after1 = data.Skip(pos1 + 1);
            var wrapped = data.Take(pos1);
            var all = after1.Union(wrapped);
            var result = string.Join("", all);
            return result;
        }
    }
}