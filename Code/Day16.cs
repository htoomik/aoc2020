using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day16
    {
        public int Solve(string input)
        {
            var cleaned = input.Trim().Replace("\r", "");
            var sections = cleaned.Split("\n\n");

            var rules = sections[0].Split("\n").Select(ParseRule).ToList();
            var myTicket = sections[1].Split("\n")[1].Split(",").Select(int.Parse).ToList();
            var otherTickets = sections[2].Split("\n").Skip(1).Select(ParseTicket).ToList();

            var errors = new List<int>();
            foreach (var ticket in otherTickets)
            {
                foreach (var value in ticket)
                {
                    var foundMatch = false;
                    foreach (var rule in rules)
                    {
                        if (rule.Range1.Start <= value && value <= rule.Range1.End)
                        {
                            foundMatch = true;
                            break;
                        }
                        if (rule.Range2.Start <= value && value <= rule.Range2.End)
                        {
                            foundMatch = true;
                            break;
                        }
                    }

                    if (!foundMatch)
                    {
                        errors.Add(value);
                    }
                }
            }

            return errors.Sum();
        }

        public long Solve2(string input)
        {
            var cleaned = input.Trim().Replace("\r", "");
            var sections = cleaned.Split("\n\n");

            var rules = sections[0].Split("\n").Select(ParseRule).ToList();
            var myTicket = sections[1].Split("\n")[1].Split(",").Select(int.Parse).ToList();
            var otherTickets = sections[2].Split("\n").Skip(1).Select(ParseTicket).ToList();

            var validTickets = new List<List<int>>();
            foreach (var ticket in otherTickets)
            {
                var allValid = true;
                foreach (var value in ticket)
                {
                    var foundMatch = false;
                    foreach (var rule in rules)
                    {
                        if (RuleMatchesValue(rule, value))
                        {
                            foundMatch = true;
                            break;
                        }
                    }

                    if (!foundMatch)
                    {
                        allValid = false;
                        break;
                    }
                }

                if (allValid)
                {
                    validTickets.Add(ticket);
                }
            }

            var unmatchedRules = rules.Select(r => r).ToList();
            var valuesPerPosition = GetValuesPerPosition(myTicket, validTickets);
            var rulePositions = new Dictionary<Rule, int>();

            var done = false;
            while (!done)
            {
                for (var i = 0; i < valuesPerPosition.Count; i++)
                {
                    var position = valuesPerPosition[i];
                    var rulesMatched = 0;
                    Rule? match = null;
                    foreach (var rule in unmatchedRules)
                    {
                        if (position.All(value => RuleMatchesValue(rule, value)))
                        {
                            rulesMatched++;
                            match = rule;
                        }
                    }

                    if (rulesMatched == 1)
                    {
                        rulePositions[match.Value] = i;
                        unmatchedRules.Remove(match.Value);
                    }
                }

                done = !unmatchedRules.Any();
            }

            long total = 1;
            foreach (var (rule, position) in rulePositions)
            {
                if (rule.Name.StartsWith("departure"))
                {
                    var value = myTicket[position];
                    total *= value;
                }
            }

            return total;
        }

        private static bool RuleMatchesValue(Rule rule, int value)
        {
            if (rule.Range1.Start <= value && value <= rule.Range1.End)
            {
                return true;
            }
            if (rule.Range2.Start <= value && value <= rule.Range2.End)
            {
                return true;
            }
            return false;
        }

        private static List<List<int>> GetValuesPerPosition(List<int> myTicket, List<List<int>> validTickets)
        {
            var valuesPerPosition = new List<List<int>>();
            foreach (var i in myTicket)
            {
                valuesPerPosition.Add(new List<int>());
            }

            foreach (var ticket in validTickets)
            {
                for (int i = 0; i < ticket.Count; i++)
                {
                    valuesPerPosition[i].Add(ticket[i]);
                }
            }

            return valuesPerPosition;
        }

        private List<int> ParseTicket(string s)
        {
            return s.Split(",").Select(int.Parse).ToList();
        }

        private Rule ParseRule(string s)
        {
            var parts = s.Split(":");
            var name = parts[0];

            var ranges = parts[1].Split(" ");
            var range1 = ParseRange(ranges[1]);
            var range2 = ParseRange(ranges[3]);

            return new Rule(name, range1, range2);
        }

        private Range ParseRange(string s)
        {
            var parts = s.Split("-");
            return new Range(int.Parse(parts[0]), int.Parse(parts[1]));
        }

        private struct Rule
        {
            public string Name { get; }
            public Range Range1 { get; }
            public Range Range2 { get; }

            public Rule(string name, Range range1, Range range2)
            {
                Name = name;
                Range1 = range1;
                Range2 = range2;
            }
        }

        private struct Range
        {
            public int Start { get; }
            public int End { get; }

            public Range(int start, int end)
            {
                Start = start;
                End = end;
            }

        }
    }
}