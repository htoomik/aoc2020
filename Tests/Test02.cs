using System.Collections.Generic;
using System.Linq;
using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test02
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test02(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Part1()
        {
            var input = @"
1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc".ChopToList();
            var solver = new Day02();
            var result = solver.Solve(input);
            result.ShouldBe(2);
        }

        [Fact]
        public void Part2()
        {
            var input = @"
1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc".ChopToList();
            var solver = new Day02();
            var result = solver.Solve2(input);
            result.ShouldBe(1);
        }

        [Fact]
        public void Solve()
        {
            var solver = new Day02();
            var input = DataHelper.GetAllRows(2);
            var result = solver.Solve(input);
            _testOutputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var solver = new Day02();
            var input = DataHelper.GetAllRows(2);
            var result = solver.Solve2(input);
            _testOutputHelper.WriteLine(result.ToString());
        }
    }
}