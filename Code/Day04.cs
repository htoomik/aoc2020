using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2020.Code
{
    public class Day04
    {
        public int Solve(string input)
        {
            var cleaned = input.Replace("\r\n", "\n");

            var passports = cleaned.Split("\n\n").Select(Parse);
            var valid = passports.Count(p => p.Valid);

            return valid;
        }

        public int Solve2(string input)
        {
            var cleaned = input.Replace("\r\n", "\n");

            var passports = cleaned.Split("\n\n").Select(Parse);
            var valid = passports.Count(p => p.Valid2);

            return valid;
        }

        private Passport Parse(string data)
        {
            var cleaned = data.Replace("\n", " ");
            var parts = cleaned.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var passport = new Passport();
            foreach (var part in parts)
            {
                passport.SetValue(part.Split(":"));
            }

            return passport;
        }

        private class Passport
        {
            private readonly Dictionary<string, string> _values = new Dictionary<string, string>();

            private static readonly HashSet<string> Keys = new HashSet<string>
                { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid" };

            public void SetValue(string[] valuePair)
            {
                _values[valuePair[0]] = valuePair[1];
            }

            public bool Valid
            {
                get
                {
                    var keys = _values.Keys;
                    var missingKeys = Keys.Except(keys).ToList();

                    return missingKeys.Count switch
                    {
                        0 => true,
                        1 => missingKeys.Single() == "cid",
                        _ => false
                    };
                }
            }

            public bool Valid2
            {
                get
                {
                    if (!Valid)
                    {
                        return false;
                    }

                    return ByrValid && IyrValid && EyrValid && HgtValid && HclValid && EclValid && PidValid;
                }
            }

            private bool ByrValid => IsValidYear(_values["byr"], 1920, 2002);
            private bool IyrValid => IsValidYear(_values["iyr"], 2010, 2020);
            private bool EyrValid => IsValidYear(_values["eyr"], 2020, 2030);
            private bool HclValid => Regex.IsMatch(_values["hcl"], @"^#[a-f0-9]{6}$");
            private bool EclValid => Regex.IsMatch(_values["ecl"], @"^amb|blu|brn|gry|grn|hzl|oth$");
            private bool PidValid => Regex.IsMatch(_values["pid"], @"^\d{9}$");

            private bool HgtValid
            {
                get
                {
                    var s = _values["hgt"];
                    var regex = new Regex(@"^(\d+)(in|cm)$");
                    if (!regex.IsMatch(s))
                    {
                        return false;
                    }

                    var match = regex.Match(s);
                    var height = int.Parse(match.Groups[1].Value);
                    var units = match.Groups[2].Value;

                    return units switch
                    {
                        "cm" => 150 <= height && height <= 193,
                        "in" => 59 <= height && height <= 76,
                        _ => false
                    };
                }
            }

            private bool IsValidYear(string s, int min, int max)
            {
                if (!int.TryParse(s, out var y))
                {
                    return false;
                }

                return min <= y && y <= max;
            }
        }
    }
}