using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day08
    {
        public int Solve(List<string> input)
        {
            var instructions = input.Select(Parse).ToList();
            return Run(instructions).Item2;
        }

        private static (bool, int) Run(List<Instruction> instructions)
        {
            var visited = new HashSet<int>();
            var acc = 0;
            var index = 0;

            var terminated = false;

            while (true)
            {
                if (visited.Contains(index))
                {
                    break;
                }

                if (index >= instructions.Count())
                {
                    terminated = true;
                    break;
                }

                visited.Add(index);
                var op = instructions[index];
                switch (op.Op)
                {
                    case "nop":
                        index++;
                        break;
                    case "acc":
                        index++;
                        acc += op.Arg;
                        break;
                    case "jmp":
                        index += op.Arg;
                        break;
                    default:
                        throw new Exception();
                }
            }

            return (terminated, acc);
        }

        public int Solve2(List<string> input)
        {
            var instructions = input.Select(Parse).ToList();

            var allNops = new List<int>();
            var allJmps = new List<int>();

            for (int i = 0; i < instructions.Count(); i++)
            {
                switch (instructions[i].Op)
                {
                    case "jmp":
                        allJmps.Add(i);
                        break;
                    case "nop":
                        allNops.Add(i);
                        break;
                }
            }

            foreach (var jmp in allJmps)
            {
                var newInstructions = new List<Instruction>();
                newInstructions.AddRange(instructions.Take(jmp));
                newInstructions.Add(new Instruction("nop", instructions[jmp].Arg));
                newInstructions.AddRange(instructions.Skip(jmp + 1));

                if (newInstructions.Count() != instructions.Count())
                {
                    throw new Exception();
                }

                var result = Run(newInstructions);
                if (result.Item1)
                {
                    return result.Item2;
                }
            }

            foreach (var nop in allNops)
            {
                var newInstructions = new List<Instruction>();
                newInstructions.AddRange(instructions.Take(nop));
                newInstructions.Add(new Instruction("jmp", instructions[nop].Arg));
                newInstructions.AddRange(instructions.Skip(nop + 1));

                if (newInstructions.Count() != instructions.Count())
                {
                    throw new Exception();
                }

                var result = Run(newInstructions);
                if (result.Item1)
                {
                    return result.Item2;
                }
            }

            throw new Exception("No solution found");
        }

        private static Instruction Parse(string input)
        {
            var parts = input.Split(' ');
            return new Instruction(parts[0], int.Parse(parts[1]));
        }

        private readonly struct Instruction
        {
            public string Op { get; }
            public int Arg { get; }

            public Instruction(string op, int arg)
            {
                Op = op;
                Arg = arg;
            }
        }
    }
}