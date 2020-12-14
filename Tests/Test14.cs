using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test14 : TestBase
    {
        public Test14(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            var input = @"
mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0".ChopToList();
            var solver = new Day14();
            var result = solver.Solve(input);
            result.ShouldBe(165);
        }

        [Fact]
        public void Part2()
        {
            var input = @"
mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1".ChopToList();
            var solver = new Day14();
            var result = solver.Solve2(input);
            result.ShouldBe(208);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.GetAllRows(14);
            var solver = new Day14();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.GetAllRows(14);
            var solver = new Day14();
            var result = solver.Solve2(input);
            Output.WriteLine(result.ToString());
        }
    }
}