using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test16 : TestBase
    {
        public Test16(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            var input = @"
class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12".Trim();
            var solver = new Day16();
            var result = solver.Solve(input);
            result.ShouldBe(71);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.Get(16);
            var solver = new Day16();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.Get(16);
            var solver = new Day16();
            var result = solver.Solve2(input);
            Output.WriteLine(result.ToString());
        }
    }
}