using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2020.Code
{
    public class Day19
    {
        public int Solve(string input, bool overrideRules)
        {
            var cleaned = input.Replace("\r", "");
            var sections = cleaned.Split("\n\n");
            var dict = ParseRules(sections[0]);

            if (overrideRules)
            {
                dict[8] = "42 | " +
                          "42 42 | " +
                          "42 42 42 | " +
                          "42 42 42 42 | " +
                          "42 42 42 42 42 | " +
                          "42 42 42 42 42 42";
                dict[11] = "42 31 | " +
                           "42 42 31 31 | " +
                           "42 42 42 31 31 31 | " +
                           "42 42 42 42 31 31 31 31 | " +
                           "42 42 42 42 42 31 31 31 31 31 |" +
                           "42 42 42 42 42 42 31 31 31 31 31 31";
            }

            var expanded = Expand(dict[0], dict);
            var clean = expanded.Replace(" ", "");
            var pattern = $"^({clean})$";
            var regex = new Regex(pattern);

            var messages = sections[1].Split("\n").ToList();
            var matches = messages.Count(m => regex.IsMatch(m));

            return matches;
        }

        private Dictionary<int, string> ParseRules(string section)
        {
            var rules = section.Split("\n").Select(ParseRule).ToList();
            var dict = rules.ToDictionary(t => t.Item1, t => t.Item2);
            return dict;
        }

        private string Expand(string rule0, Dictionary<int, string> rules)
        {
            var parts = rule0.Split(" ");
            var newParts = new List<string>();
            foreach (var part in parts)
            {
                if (int.TryParse(part, out var i))
                {
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