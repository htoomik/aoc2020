using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day11
    {
        public int Solve(List<string> input)
        {
            var seats = input.Select(s => s.ToCharArray()).ToArray();

            var rows = input.Count;
            var cols = input[0].Length;

            while (true)
            {
                var newSeats = new char[rows][];
                var allSame = true;

                for (var i = 0; i < rows; i++)
                {
                    newSeats[i] =new char[cols];
                    for (var j = 0; j < cols; j++)
                    {
                        var previousState = seats[i][j];
                        var newState = Calculate(seats, i, j);

                        newSeats[i][j] = newState;
                        if (newState != previousState)
                        {
                            allSame = false;
                        }
                    }
                }

                if (allSame)
                {
                    return newSeats.Sum(r => r.Count(c => c == '#'));
                }

                seats = newSeats;
            }
        }

        public int Solve2(List<string> input)
        {
            var seats = input.Select(s => s.ToCharArray()).ToArray();

            var rows = input.Count;
            var cols = input[0].Length;

            while (true)
            {
                var newSeats = new char[rows][];
                var allSame = true;

                for (var i = 0; i < rows; i++)
                {
                    newSeats[i] =new char[cols];
                    for (var j = 0; j < cols; j++)
                    {
                        var previousState = seats[i][j];
                        var newState = Calculate2(seats, i, j);

                        newSeats[i][j] = newState;
                        if (newState != previousState)
                        {
                            allSame = false;
                        }
                    }
                }

                if (allSame)
                {
                    return newSeats.Sum(r => r.Count(c => c == '#'));
                }

                seats = newSeats;
            }
        }

        private char Calculate(char[][] seats, in int i, in int j)
        {
            var state = seats[i][j];
            if (state == '.')
            {
                return '.';
            }

            var occupiedSeats = new List<bool>
            {
                IsOccupied(seats, i - 1, j),
                IsOccupied(seats, i + 1, j),
                IsOccupied(seats, i, j - 1),
                IsOccupied(seats, i, j + 1),
                IsOccupied(seats, i - 1, j - 1),
                IsOccupied(seats, i - 1, j + 1),
                IsOccupied(seats, i + 1, j - 1),
                IsOccupied(seats, i + 1, j + 1)
            };

            var occupiedCount = occupiedSeats.Count(s => s);
            if (state == 'L' && occupiedCount == 0)
            {
                return '#';
            }

            if (state == '#' && occupiedCount >= 4)
            {
                return 'L';
            }

            return state;
        }

        private char Calculate2(char[][] seats, in int i, in int j)
        {
            var state = seats[i][j];
            if (state == '.')
            {
                return '.';
            }

            var nearestSeats = new List<(int, int)>
            {
                FindSeat(seats, i, j, -1, -1),
                FindSeat(seats, i, j, -1,  0),
                FindSeat(seats, i, j, -1, +1),
                FindSeat(seats, i, j,  0, -1),
                FindSeat(seats, i, j,  0, +1),
                FindSeat(seats, i, j, +1, -1),
                FindSeat(seats, i, j, +1,  0),
                FindSeat(seats, i, j, +1, +1),
            };

            var occupiedCount = nearestSeats.Select(s => IsOccupied(seats, s.Item1, s.Item2)).Count(s => s);

            if (state == 'L' && occupiedCount == 0)
            {
                return '#';
            }

            if (state == '#' && occupiedCount >= 5)
            {
                return 'L';
            }

            return state;
        }

        private static (int, int) FindSeat(char[][] seats, int i, int j, int diffI, int diffJ)
        {
            var posI = i;
            var posJ = j;
            while (true)
            {
                posI += diffI;
                posJ += diffJ;

                if (posI < 0 ||
                    posJ < 0 ||
                    posI >= seats.Length ||
                    posJ >= seats[0].Length)
                {
                    return (-1, -1);
                }

                var pos = seats[posI][posJ];
                if (pos == 'L' || pos == '#')
                {
                    return (posI, posJ);
                }
            }
        }

        private static bool IsOccupied(char[][] seats, int i, int j)
        {
            if (i < 0) return false;
            if (j < 0) return false;
            if (i >= seats.Length) return false;
            if (j >= seats[i].Length) return false;

             return seats[i][j] == '#';
        }
    }
}