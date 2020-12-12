using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test11 : TestBase
    {
        public Test11(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            var input = @"
L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL".ChopToList();
            var solver = new Day11();
            var result = solver.Solve(input);
            result.ShouldBe(37);
        }

        [Fact]
        public void Part2()
        {
            var input = @"
L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL".ChopToList();
            var solver = new Day11();
            var result = solver.Solve2(input);
            result.ShouldBe(26);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.GetAllRows(11);
            var solver = new Day11();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.GetAllRows(11);
            var solver = new Day11();
            var result = solver.Solve2(input);
            Output.WriteLine(result.ToString());
        }
    }
}