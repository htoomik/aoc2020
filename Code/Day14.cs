using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace aoc2020.Code
{
    public class Day14
    {
        public long Solve(List<string> input)
        {
            var mask = ParseMask(input[0]);
            var values = new Dictionary<int, long>();

            foreach (var line in input)
            {
                if (line.StartsWith("mask"))
                {
                    mask = ParseMask(line);
                }
                else if (line.StartsWith("mem"))
                {
                    var instruction = ParseInstruction(line);
                    var masked = Mask(instruction.Value, mask);
                    values[instruction.Index] = masked;
                }
                else
                {
                    throw new Exception();
                }
            }

            return values.Values.Sum();
        }

        public long Solve2(List<string> input)
        {
            var mask = ParseMask(input[0]);
            var values = new Dictionary<long, long>();

            foreach (var line in input)
            {
                if (line.StartsWith("mask"))
                {
                    mask = ParseMask(line);
                }
                else if (line.StartsWith("mem"))
                {
                    var instruction = ParseInstruction(line);
                    var matches = GetMatches(instruction.Index, mask);

                    foreach (var address in matches)
                    {
                        values[address] = instruction.Value;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }

            return values.Values.Sum();
        }

        private char[] ParseMask(string input)
        {
            return input.Replace("mask = ", "").ToCharArray();
        }

        private Instruction ParseInstruction(string input)
        {
            var parts = input.Split(" = ");
            var index = int.Parse(parts[0].Replace("mem[", "").Replace("]", ""));
            var value = long.Parse(parts[1]);
            return new Instruction(index, value);
        }

        private long Mask(long value, char[] mask)
        {
            var reversed = mask.Reverse();
            long n = 1;

            foreach (var bit in reversed)
            {
                if (bit == '0')
                {
                    value &= ~n;
                }

                if (bit == '1')
                {
                    value |= n;
                }

                n *= 2;
            }

            return value;
        }

        private HashSet<long> GetMatches(long value, char[] mask)
        {
            var reversed = mask.Reverse().ToArray();
            long n = 1;

            foreach (var bit in reversed)
            {
                if (bit == '1')
                {
                    value |= n;
                }

                n *= 2;
            }

            var results = new HashSet<long>();
            results.Add(value);
            n = 1;
            foreach (var bit in reversed)
            {
                if (bit == 'X')
                {
                    var newResults = new List<long>();
                    foreach (var result in results)
                    {
                        newResults.Add(result & ~n);
                        newResults.Add(result | n);
                    }

                    foreach (var newResult in newResults)
                    {
                        results.Add(newResult);
                    }
                }

                n *= 2;
            }

            return results;
        }

        [DebuggerDisplay("mem[{Index}] = {Value}")]
        private readonly struct Instruction
        {
            public int Index { get; }
            public long Value { get; }

            public Instruction(int index, long value)
            {
                Index = index;
                Value = value;
            }
        }
    }
}