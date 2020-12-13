using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day12Part2
    {
        public int Solve(List<string> input)
        {
            var ship = new Ship
            {
                X = 0,
                Y = 0,
            };
            var waypoint = new Waypoint
            {
                X = 10,
                Y = -1
            };

            var instructions = input.Select(Parse);
            foreach (var instruction in instructions)
            {
                Apply(ship, waypoint, instruction);
            }

            return Math.Abs(ship.X) + Math.Abs(ship.Y);
        }

        private static void Apply(Ship ship, Waypoint waypoint, Tuple<char, int> instruction)
        {
            switch (instruction.Item1)
            {
                case 'N':
                    waypoint.Move(Direction.North, instruction.Item2);
                    break;
                case 'S':
                    waypoint.Move(Direction.South, instruction.Item2);
                    break;
                case 'E':
                    waypoint.Move(Direction.East, instruction.Item2);
                    break;
                case 'W':
                    waypoint.Move(Direction.West, instruction.Item2);
                    break;
                case 'F':
                    ship.Move(waypoint, instruction.Item2);
                    break;
                case 'L':
                    waypoint.Rotate(-instruction.Item2);
                    break;
                case 'R':
                    waypoint.Rotate(instruction.Item2);
                    break;
            }
        }

        private Tuple<char, int> Parse(string input)
        {
            return new Tuple<char, int>(input[0], int.Parse(input.Substring(1)));
        }

        private class Ship
        {
            public int X;
            public int Y;

            public void Move(Waypoint waypoint, int steps)
            {
                for (var i = 0; i < steps; i++)
                {
                    X += waypoint.X;
                    Y += waypoint.Y;
                }
            }
        }

        public class Waypoint
        {
            public int X;
            public int Y;

            public void Move(Direction direction, int steps)
            {
                switch (direction)
                {
                    case Direction.North:
                        Y -= steps;
                        break;
                    case Direction.South:
                        Y += steps;
                        break;
                    case Direction.East:
                        X += steps;
                        break;
                    case Direction.West:
                        X -= steps;
                        break;
                }
            }

            public void Rotate(int degrees)
            {
                degrees += 360;
                degrees = degrees % 360;

                while (degrees > 0)
                {

                    var absX = Math.Abs(X);
                    var absY = Math.Abs(Y);
                    if (X > 0 && Y > 0)
                    {
                        X = -absY;
                        Y = absX;
                    }
                    else if (X > 0 && Y <= 0)
                    {
                        X = absY;
                        Y = absX;
                    }
                    else if (X <= 0 && Y > 0)
                    {
                        X = -absY;
                        Y = -absX;
                    }
                    else if (X <= 0 && Y <= 0)
                    {
                        X = absY;
                        Y = -absX;
                    }

                    degrees -= 90;
                }
            }
        }

        public enum Direction
        {
            East,
            South,
            West,
            North
        }
    }
}