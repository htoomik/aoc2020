using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2020.Code
{
    public class Day19Part2
    {
        public int Solve(string input)
        {
            var cleaned = input.Replace("\r", "");
            var sections = cleaned.Split("\n\n");
            var dict = ParseRules(sections[0]);

            var rule0 = Expand(dict[0], dict).Replace(" ", "");
            var rule31 = Expand(dict[31], dict).Replace(" ", "");
            var rule42 = Expand(dict[42], dict).Replace(" ", "");

            var rule8 = $"({rule42})*";
            var rule11 = $"({rule42})*({rule31})*";

            var fullRule = rule0.Replace("8", rule8).Replace("11", rule11);
            var simplified = fullRule.Replace("(a)", "a").Replace("(b)", "b");
            var pattern = $"^({simplified})$";
            var regex = new Regex(pattern);

            var messages = sections[1].Split("\n").ToList();
            var matches = messages.Count(m => regex.IsMatch(m));

            return matches;
        }

        public Dictionary<int, string> ParseRules(string section)
        {
            var rules = section.Split("\n").Select(ParseRule).ToList();
            var dict = rules.ToDictionary(t => t.Item1, t => t.Item2);
            return dict;
        }

        public string Expand(string rule0, Dictionary<int, string> rules)
        {
            var parts = rule0.Split(" ");
            var newParts = new List<string>();
            foreach (var part in parts)
            {
                if (int.TryParse(part, out var i))
                {
                    if (i == 8 || i == 11)
                    {
                        newParts.Add(i.ToString());
                    }

                    var rule = rules[i];
                    newParts.Add(Expand(rule, rules));
                }
                else
                {
                    newParts.Add(part);
                }
            }

            var newRule = string.Join(" ", newParts);
            return "(" + newRule + ")";
        }

        private Tuple<int, string> ParseRule(string input)
        {
            var parts = input.Split(":");
            var id = int.Parse(parts[0]);
            var value = parts[1].Replace("\"", "");
            return new Tuple<int, string>(id, value);
        }
    }
}