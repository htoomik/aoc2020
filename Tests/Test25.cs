using aoc2020.Code;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test25 : TestBase
    {
        public Test25(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            var solver = new Day25();
            var result = solver.Solve(5764801, 17807724);
            result.ShouldBe(14897079);
        }

        [Fact]
        public void Solve()
        {
            var solver = new Day25();
            var result = solver.Solve(15113849, 4206373);
            Output.WriteLine(result.ToString());
        }
    }
}