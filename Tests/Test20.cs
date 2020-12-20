using System.IO;
using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test20 : TestBase
    {
        public Test20(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            var input = File.ReadAllText("C:\\Code\\aoc2020\\Data\\test20.txt");
            var solver = new Day20();
            var result = solver.Solve(input);
            result.ShouldBe(20899048083289);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.Get(20);
            var solver = new Day20();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }
    }
}