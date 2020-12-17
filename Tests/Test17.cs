using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test17 : TestBase
    {
        public Test17(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            var input = @"
.#.
..#
###".ChopToList();
            var solver = new Day17();
            var result = solver.Solve(input);
            result.ShouldBe(112);
        }

        [Fact]
        public void Part2()
        {
            var input = @"
.#.
..#
###".ChopToList();
            var solver = new Day17Part2();
            var result = solver.Solve(input);
            result.ShouldBe(848);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.GetAllRows(17);
            var solver = new Day17();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.GetAllRows(17);
            var solver = new Day17Part2();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }
    }
}