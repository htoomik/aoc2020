using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day17
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

        private Dictionary<Xyz, bool> Parse(List<string> input)
        {
            var result = new Dictionary<Xyz, bool>();

            for (var y = 0; y < input.Count; y++)
            {
                for (var x = 0; x < input[0].Length; x++)
                {
                    var value = input[y][x];
                    var on = value == '#';
                    result.Add(new Xyz(x, y, 0), on);
                }
            }

            return result;
        }

        private Dictionary<Xyz, bool> Mutate(Dictionary<Xyz,bool> state)
        {
            var newState = new Dictionary<Xyz, bool>();

            var minX = state.Keys.Min(k => k.X);
            var minY = state.Keys.Min(k => k.Y);
            var minZ = state.Keys.Min(k => k.Z);

            var maxX = state.Keys.Max(k => k.X);
            var maxY = state.Keys.Max(k => k.Y);
            var maxZ = state.Keys.Max(k => k.Z);

            for (var z = minZ - 1; z <= maxZ + 1; z++)
            {
                for (var y = minY - 1; y <= maxY + 1; y++)
                {
                    for (var x = minX - 1; x <= maxX + 1; x++)
                    {
                        var newValue = GetValue(state, x, y, z);
                        newState.Add(new Xyz(x, y, z), newValue);
                    }
                }
            }

            return newState;
        }

        private bool GetValue(Dictionary<Xyz,bool> state, in int x, in int y, in int z)
        {
            var xyz = new Xyz(x, y, z);
            var currentValue = state.ContainsKey(xyz) && state[xyz];
            var count = GetActiveNeighboursCount(state, x, y, z);

            return currentValue
                ? count == 2 || count == 3
                : count == 3;
        }

        private static int GetActiveNeighboursCount(Dictionary<Xyz, bool> state, int x, int y, int z)
        {
            var count = 0;
            for (var diffX = -1; diffX <= 1; diffX++)
            {
                for (var diffY = -1; diffY <= 1; diffY++)
                {
                    for (var diffZ = -1; diffZ <= 1; diffZ++)
                    {
                        if (diffX == 0 && diffY == 0 && diffZ == 0)
                        {
                            continue;
                        }

                        var xyz = new Xyz(x + diffX, y + diffY, z + diffZ);
                        var exists = state.TryGetValue(xyz, out var value);
                        if (exists && value)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        private readonly struct Xyz
        {
            public int X { get; }
            public int Y { get; }
            public int Z { get; }

            public Xyz(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }
    }
}