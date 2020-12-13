using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day12
    {
        public int Solve(List<string> input)
        {
            var ship = new Ship
            {
                X = 0,
                Y = 0,
                Direction = Direction.East,
            };
            var instructions = input.Select(Parse);
            foreach (var instruction in instructions)
            {
                Apply(ship, instruction);
            }

            return Math.Abs(ship.X) + Math.Abs(ship.Y);
        }

        private static void Apply(Ship ship, Tuple<char, int> instruction)
        {
            switch (instruction.Item1)
            {
                case 'N':
                    ship.Move(Direction.North, instruction.Item2);
                    break;
                case 'S':
                    ship.Move(Direction.South, instruction.Item2);
                    break;
                case 'E':
                    ship.Move(Direction.East, instruction.Item2);
                    break;
                case 'W':
                    ship.Move(Direction.West, instruction.Item2);
                    break;
                case 'F':
                    ship.Move(ship.Direction, instruction.Item2);
                    break;
                case 'L':
                    ship.Turn(-instruction.Item2);
                    break;
                case 'R':
                    ship.Turn(instruction.Item2);
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
            public Direction Direction;

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

            public void Turn(int degrees)
            {
                var sign = Math.Sign(degrees);
                degrees = Math.Abs(degrees);

                while (degrees > 0)
                {
                    Direction += sign;
                    if ((int)Direction < 0)
                    {
                        Direction += 4;
                    }

                    if ((int)Direction > 3)
                    {
                        Direction -= 4;
                    }

                    degrees -= 90;
                }
            }
        }

        private enum Direction
        {
            East,
            South,
            West,
            North
        }
    }
}