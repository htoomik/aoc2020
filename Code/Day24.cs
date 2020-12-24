using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day24
    {
        public int Solve(List<string> input)
        {
            var finals = Apply(input);

            return finals.Count;
        }

        private static HashSet<Tuple<int, int>> Apply(List<string> input)
        {
            var finals = new HashSet<Tuple<int, int>>();

            foreach (var line in input)
            {
                var x = 0;
                var y = 0;

                var i = 0;
                while (i < line.Length)
                {
                    var c = line[i];
                    switch (c)
                    {
                        case 'e':
                            x += 2;
                            break;
                        case 'w':
                            x -= 2;
                            break;
                        case 's':
                        {
                            y += 1;
                            var c2 = line[i + 1];
                            switch (c2)
                            {
                                case 'e':
                                    x += 1;
                                    break;
                                case 'w':
                                    x -= 1;
                                    break;
                            }

                            i++;
                            break;
                        }
                        case 'n':
                        {
                            y -= 1;
                            var c2 = line[i + 1];
                            switch (c2)
                            {
                                case 'e':
                                    x += 1;
                                    break;
                                case 'w':
                                    x -= 1;
                                    break;
                            }

                            i++;
                            break;
                        }
                    }

                    i++;
                }

                var t = new Tuple<int, int>(x, y);
                if (finals.Contains(t))
                {
                    finals.Remove(t);
                }
                else
                {
                    finals.Add(t);
                }
            }

            return finals;
        }

        public int Solve2(List<string> input, int days)
        {
            var state = Apply(input);

            for (var i = 0; i < days; i++)
            {
                state = Flip(state);
            }

            return state.Count;
        }

        private HashSet<Tuple<int, int>> Flip(HashSet<Tuple<int, int>> blacks)
        {
            var minX = blacks.Select(t => t.Item1).Min();
            var minY = blacks.Select(t => t.Item2).Min();
            var maxX = blacks.Select(t => t.Item1).Max();
            var maxY = blacks.Select(t => t.Item2).Max();

            var newState = new HashSet<Tuple<int,int>>();
            for (var x = minX - 2; x <= maxX + 2; x++)
            {
                for (var y = minY - 1; y <= maxY + 1; y++)
                {
                    var t = new Tuple<int, int>(x, y);
                    var isBlack = blacks.Contains(t);
                    var neighbours = GetNeighbours(x, y);
                    var blackNeighbours = neighbours.Count(blacks.Contains);
                    if (isBlack)
                    {
                        if (blackNeighbours == 1 ||
                            blackNeighbours == 2)
                        {
                            newState.Add(t);
                        }
                    }
                    else
                    {
                        if (blackNeighbours == 2)
                        {
                            newState.Add(t);
                        }
                    }
                }
            }

            return newState;
        }

        private static IEnumerable<Tuple<int, int>> GetNeighbours(int x, int y)
        {
            yield return new Tuple<int, int>(x - 2, y);
            yield return new Tuple<int, int>(x + 2, y);
            yield return new Tuple<int, int>(x - 1, y - 1);
            yield return new Tuple<int, int>(x - 1, y + 1);
            yield return new Tuple<int, int>(x + 1, y - 1);
            yield return new Tuple<int, int>(x + 1, y + 1);
        }
    }
}