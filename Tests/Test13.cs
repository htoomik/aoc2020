using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test13 : TestBase
    {
        public Test13(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            var input = @"
939
7,13,x,x,59,x,31,19".ChopToList();
            var solver = new Day13();
            var result = solver.Solve(input);
            result.ShouldBe(295);
        }

        [Fact]
        public void Part2()
        {
            var input = @"
939
7,13,x,x,59,x,31,19".ChopToList();
            var solver = new Day13Part2();
            var result = solver.Solve(input);
            result.ShouldBe(1068781);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.GetAllRows(13);
            var solver = new Day13();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.GetAllRows(13);
            var solver = new Day13Part2();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }
    }
}