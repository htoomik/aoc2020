using aoc2020.Code;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test15 : TestBase
    {
        public Test15(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            const string input = "0,3,6";
            var solver = new Day15();
            var result = solver.Solve(input, 2020);
            result.ShouldBe(436);
        }

        [Fact]
        public void Solve()
        {
            const string input = "14,8,16,0,1,17";
            var solver = new Day15();
            var result = solver.Solve(input, 2020);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            const string input = "14,8,16,0,1,17";
            var solver = new Day15();
            var result = solver.Solve(input, 30000000);
            Output.WriteLine(result.ToString());
        }
    }
}