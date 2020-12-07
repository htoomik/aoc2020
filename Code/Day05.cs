using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day05
    {
        public int Solve(List<string> input)
        {
            return input.Select(GetSeatId).Max();
        }

        public int Solve2(List<string> input)
        {
            var seats = input.Select(GetSeatId).ToList();
            var min = seats.Min();
            var max = seats.Max();
            for (var i = min; i < max; i++)
            {
                if (!seats.Contains(i))
                {
                    return i;
                }
            }
            throw new Exception("Not found");
        }

        public int GetSeatId(string input)
        {
            var rows = input.Substring(0, 7);
            var seats = input.Substring(7, 3);

            var row = Partition(rows, 'F', 'B', 0, 127);
            var seat = Partition(seats, 'L', 'R', 0, 7);

            return row * 8 + seat;
        }

        private int Partition(string input, char lower, char upper, int min, int max)
        {
            var half = (max - min + 1) / 2;
            foreach (var c in input.ToCharArray())
            {
                if (c == lower)
                {
                    max -= half;
                }
                else if (c == upper)
                {
                    min += half;
                }
                else
                {
                    throw new Exception("Unexpected character");
                }

                half = half / 2;
            }

            if (min != max)
            {
                throw new Exception($"Min = {min}, Max = {max}");
            }

            return min;
        }
    }
}