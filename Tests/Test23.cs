using aoc2020.Code;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test23 : TestBase
    {
        public Test23(ITestOutputHelper helper) : base(helper)
        {
        }

        [Theory]
        [InlineData(10, "92658374")]
        [InlineData(100, "67384529")]
        public void Part1(int rounds, string expected)
        {
            const string input = "389125467";
            var solver = new Day23();
            var result = solver.Solve(input, rounds);
            result.ShouldBe(expected);
        }

        [Fact]
        public void Solve()
        {
            const string input = "643719258";
            var solver = new Day23();
            var result = solver.Solve(input, 100);
            Output.WriteLine(result);
        }
    }
}