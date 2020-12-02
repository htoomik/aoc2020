using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2020.Code
{
    public class Day02
    {
        public int Solve(List<string> input)
        {
            var data = input.Select(Parse);
            var result = data.Where(Valid).Count();
            return result;
        }

        public int Solve2(List<string> input)
        {
            var data = input.Select(Parse);
            var result = data.Where(Valid2).Count();
            return result;
        }

        private static Item Parse(string input)
        {
            var regex = new Regex(@"^(\d+)-(\d+) ([a-z]+): ([a-z]+)$");
            var match = regex.Match(input);
            return new Item(
                int.Parse(match.Groups[1].Value),
                int.Parse(match.Groups[2].Value),
                match.Groups[3].Value,
                match.Groups[4].Value);
        }

        private static bool Valid(Item arg)
        {
            var originalLength = arg.Password.Length;
            var afterReplace = arg.Password.Replace(arg.Letter, "").Length;
            var diff = originalLength - afterReplace;
            return arg.Min <= diff && diff <= arg.Max;
        }

        private static bool Valid2(Item arg)
        {
            var char1 = arg.Password.Substring(arg.Min - 1, 1);
            var char2 = arg.Password.Substring(arg.Max - 1, 1);
            var match1 = char1 == arg.Letter;
            var match2 = char2 == arg.Letter;
            return match1 ^ match2;
        }

        private struct Item
        {
            public int Min { get; }
            public int Max { get; }
            public string Letter { get; }
            public string Password { get; }

            public Item(int min, int max, string letter, string password)
            {
                Min = min;
                Max = max;
                Letter = letter;
                Password = password;
            }
        }
    }
}