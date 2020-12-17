using System;
using System.Collections.Generic;

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

            var count = Count(state);
            return count;
        }

        private Array Parse(List<string> input)
        {
            const int minX = -1;
            const int minY = -1;
            const int minZ = -1;
            var maxY = input.Count;
            var maxX = input[0].Length;
            const int maxZ = 1;

            var result = Array.CreateInstance(typeof(bool),
                lengths: new[] { maxX - minX + 1, maxY - minY + 1, maxZ - minZ + 1 },
                lowerBounds: new[] { minX, minY, minZ });

            for (var y = 0; y < maxY; y++)
            {
                for (var x = 0; x < maxX; x++)
                {
                    var value = input[y][x];
                    var on = value == '#';
                    result.SetValue(on, x, y, 0);
                }
            }

            return result;
        }

        private Array Mutate(Array state)
        {
            var minX = state.GetLowerBound(0) - 1;
            var minY = state.GetLowerBound(1) - 1;
            var minZ = state.GetLowerBound(2) - 1;

            var lengthX = state.GetLength(0) + 2;
            var lengthY = state.GetLength(1) + 2;
            var lengthZ = state.GetLength(2) + 2;

            var newState = Array.CreateInstance(typeof(bool),
                lengths: new[] { lengthX, lengthY, lengthZ },
                lowerBounds: new[] { minX, minY, minZ });

            for (var z = minZ; z < minZ + lengthZ; z++)
            {
                for (var y = minY; y < minY + lengthY; y++)
                {
                    for (var x = minX; x < minX + lengthX; x++)
                    {
                        var newValue = GetValue(state, x, y, z);
                        newState.SetValue(newValue, x, y, z);
                    }
                }
            }

            return newState;
        }

        private bool GetValue(Array state, in int x, in int y, in int z)
        {
            if (x < state.GetLowerBound(0) || x > state.GetUpperBound(0))
            {
                return false;
            }

            if (y < state.GetLowerBound(1) || y > state.GetUpperBound(1))
            {
                return false;
            }

            if (z < state.GetLowerBound(2) || z > state.GetUpperBound(2))
            {
                return false;
            }

            var currentValue = GetBoolean(state, x, y, z);
            var count = GetActiveNeighboursCount(state, x, y, z);

            return currentValue
                ? count == 2 || count == 3
                : count == 3;
        }

        private static int GetActiveNeighboursCount(Array state, int x, int y, int z)
        {
            var count = 0;
            for (var diffX = -1; diffX <= 1; diffX++)
            {
                var newX = x + diffX;
                if (newX < state.GetLowerBound(0) || newX > state.GetUpperBound(0))
                {
                    continue;
                }

                for (var diffY = -1; diffY <= 1; diffY++)
                {
                    var newY = y + diffY;

                    if (newY < state.GetLowerBound(1) || newY > state.GetUpperBound(1))
                    {
                        continue;
                    }

                    for (var diffZ = -1; diffZ <= 1; diffZ++)
                    {
                        var newZ = z + diffZ;

                        if (newZ < state.GetLowerBound(2) || newZ > state.GetUpperBound(2))
                        {
                            continue;
                        }

                        if (diffX == 0 && diffY == 0 && diffZ == 0)
                        {
                            continue;
                        }

                        var value = GetBoolean(state, newX, newY, newZ);
                        if (value)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        private int Count(Array state)
        {
            var count = 0;
            for (var x = state.GetLowerBound(0); x <= state.GetUpperBound(0); x++)
            {
                for (var y = state.GetLowerBound(1); y <= state.GetUpperBound(1); y++)
                {
                    for (var z = state.GetLowerBound(2); z <= state.GetUpperBound(2); z++)
                    {
                        var value = GetBoolean(state, x, y, z);
                        if (value)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        private static bool GetBoolean(Array state, int x, int y, int z)
        {
            // ReSharper disable once PossibleNullReferenceException
            return (bool)state.GetValue(x, y, z);
        }
    }
}