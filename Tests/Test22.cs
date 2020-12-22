using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test22 : TestBase
    {
        public Test22(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            var input = @"
Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10".Trim();
            var solver = new Day22();
            var result = solver.Solve(input);
            result.ShouldBe(306);
        }

        [Fact]
        public void Part2()
        {
            var input = @"
Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10".Trim();
            var solver = new Day22();
            var result = solver.Solve2(input);
            result.ShouldBe(291);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.Get(22);
            var solver = new Day22();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.Get(22);
            var solver = new Day22();
            var result = solver.Solve2(input);
            Output.WriteLine(result.ToString());
        }
    }
}