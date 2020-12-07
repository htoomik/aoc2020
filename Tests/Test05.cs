using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test05 : TestBase
    {
        public Test05(ITestOutputHelper helper) : base(helper)
        {
        }

        [Theory]
        [InlineData("FBFBBFFRLR", 357)]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData("BBFFBBFRLL", 820)]
        public void Part1(string input, int expected)
        {
            var solver = new Day05();
            var result = solver.GetSeatId(input);
            result.ShouldBe(expected);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.GetAllRows(5);
            var solver = new Day05();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.GetAllRows(5);
            var solver = new Day05();
            var result = solver.Solve2(input);
            Output.WriteLine(result.ToString());
        }
    }
}