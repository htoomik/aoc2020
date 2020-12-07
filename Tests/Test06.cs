using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test06 : TestBase
    {
        public Test06(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            const string input = @"
abc

a
b
c

ab
ac

a
a
a
a

b";
            var solver = new Day06();
            var result = solver.Solve(input);
            result.ShouldBe(11);
        }

        [Fact]
        public void Part2()
        {
            var input = @"
abc

a
b
c

ab
ac

a
a
a
a

b".Trim();
            var solver = new Day06();
            var result = solver.Solve2(input);
            result.ShouldBe(6);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.Get(6);
            var solver = new Day06();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.Get(6);
            var solver = new Day06();
            var result = solver.Solve2(input);
            Output.WriteLine(result.ToString());
            // 3264 too low
        }
    }
}