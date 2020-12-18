using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test18 : TestBase
    {
        public Test18(ITestOutputHelper helper) : base(helper)
        {
        }

        [Theory]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", 71)]
        [InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51)]
        [InlineData("2 * 3 + (4 * 5)", 26)]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437)]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)]
        public void Part1(string input, int expected)
        {
            var solver = new Day18();
            var result = solver.Solve(input);
            result.ShouldBe(expected);
        }

        [Theory]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", 231)]
        [InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51)]
        [InlineData("2 * 3 + (4 * 5)", 46)]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 1445)]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669060)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340)]
        [InlineData("6 * 5 + 6 * 9 * 4", 2376)]
        [InlineData("(9 + (5 + 2 + 2 * 4) * (7 + 7 * 5 * 3) + 7) + 2 + 4 * 2 + 3 * (8 + 5)", 635115)]
        public void Part2(string input, int expected)
        {
            var solver = new Day18Part2();
            var result = solver.Solve(input);
            result.ShouldBe(expected);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.GetAllRows(18);
            var solver = new Day18();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.GetAllRows(18);
            var solver = new Day18Part2();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }
    }
}