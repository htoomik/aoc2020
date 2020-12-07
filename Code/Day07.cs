using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2020.Code
{
    public class Day07
    {
        public int Solve(List<string> input)
        {
            var rules = input.Select(Parse).ToList();

            var chains = new Dictionary<string, List<string>>();
            foreach (var rule in rules)
            {
                foreach (var inner in rule.Inner)
                {
                    var i = inner.Item2;
                    if (!chains.ContainsKey(i))
                    {
                        chains[i] = new List<string>();
                    }

                    chains[i].Add(rule.Outer);
                }
            }

            var result = new HashSet<string>();
            var frontier = new Queue<string>();
            frontier.Enqueue("shiny gold");

            while (frontier.TryDequeue(out var toCheck))
            {
                result.Add(toCheck);
                if (chains.TryGetValue(toCheck, out var toAdd))
                {
                    foreach (var t in toAdd)
                    {
                        frontier.Enqueue(t);
                    }
                }
            }

            return result.Count - 1; // remove shiny gold itself
        }

        public int Solve2(List<string> input)
        {
            var rules = input.Select(Parse);
            var dict = rules.ToDictionary(r => r.Outer, r => r);
            var total = Count(dict, "shiny gold");
            return total;
        }

        private int Count(Dictionary<string, Rule> dict, string start)
        {
            var total = 0;
            foreach (var inner in dict[start].Inner)
            {
                total += inner.Item1 * (1 + Count(dict, inner.Item2));
            }

            return total;
        }

        private Rule Parse(string arg)
        {
            var parts = arg.Split(" bags contain");

            var outer = parts[0];

            var regex = new Regex(@"(\d+) (\w+ \w+) bag");
            var match = regex.Matches(arg);

            var inners = new List<Tuple<int, string>>();
            foreach (Match m in match)
            {
                inners.Add(new Tuple<int, string>(int.Parse(m.Groups[1].Value), m.Groups[2].Value));
            }

            return new Rule(outer, inners);
        }

        public struct Rule
        {
            public string Outer;
            public List<Tuple<int, string>> Inner;

            public Rule(string outer, List<Tuple<int, string>> inner)
            {
                Outer = outer;
                Inner = inner;
            }
        }
    }
}