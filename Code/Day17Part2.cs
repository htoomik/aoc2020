using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day17Part2
    {
        public int Solve(List<string> input)
        {
            var state = Parse(input);

            for (var i = 0; i < 6; i++)
            {
                state = Mutate(state);
            }

            return state.Values.Count(v => v);
        }

        private Dictionary<Xyzw, bool> Parse(List<string> input)
        {
            var result = new Dictionary<Xyzw, bool>();

            for (var y = 0; y < input.Count; y++)
            {
                for (var x = 0; x < input[0].Length; x++)
                {
                    var value = input[y][x];
                    var on = value == '#';
                    result.Add(new Xyzw(x, y, 0, 0), on);
                }
            }

            return result;
        }

        private Dictionary<Xyzw, bool> Mutate(Dictionary<Xyzw,bool> state)
        {
            var newState = new Dictionary<Xyzw, bool>();
            var minX = state.Keys.Min(k => k.X);
            var minY = state.Keys.Min(k => k.Y);
            var minZ = state.Keys.Min(k => k.Z);
            var minW = state.Keys.Min(k => k.W);

            var maxX = state.Keys.Max(k => k.X);
            var maxY = state.Keys.Max(k => k.Y);
            var maxZ = state.Keys.Max(k => k.Z);
            var maxW = state.Keys.Max(k => k.W);

            for (var w = minW - 1; w <= maxW + 1; w++)
            {
                for (var z = minZ - 1; z <= maxZ + 1; z++)
                {
                    for (var y = minY - 1; y <= maxY + 1; y++)
                    {
                        for (var x = minX - 1; x <= maxX + 1; x++)
                        {
                            var newValue = GetValue(state, x, y, z, w);
                            newState.Add(new Xyzw(x, y, z, w), newValue);
                        }
                    }
                }
            }

            return newState;
        }

        private bool GetValue(Dictionary<Xyzw,bool> state, in int x, in int y, in int z, in int w)
        {
            var xyzw = new Xyzw(x, y, z, w);
            var currentValue = state.ContainsKey(xyzw) && state[xyzw];
            var count = GetActiveNeighboursCount(state, x, y, z, w);

            return currentValue
                ? count == 2 || count == 3
                : count == 3;
        }

        private static int GetActiveNeighboursCount(Dictionary<Xyzw, bool> state, int x, int y, int z, int w)
        {
            var count = 0;
            for (var diffX = -1; diffX <= 1; diffX++)
            {
                for (var diffY = -1; diffY <= 1; diffY++)
                {
                    for (var diffZ = -1; diffZ <= 1; diffZ++)
                    {
                        for (var diffW = -1; diffW <= 1; diffW++)
                        {
                            if (diffX == 0 && diffY == 0 && diffZ == 0 && diffW == 0)
                            {
                                continue;
                            }

                            var xyzw = new Xyzw(x + diffX, y + diffY, z + diffZ, w + diffW);
                            var exists = state.TryGetValue(xyzw, out var value);
                            if (exists && value)
                            {
                                count++;
                            }
                        }
                    }
                }
            }

            return count;
        }

        private readonly struct Xyzw
        {
            public int X { get; }
            public int Y { get; }
            public int Z { get; }
            public int W { get; }

            public Xyzw(int x, int y, int z, int w)
            {
                X = x;
                Y = y;
                Z = z;
                W = w;
            }
        }
    }
}