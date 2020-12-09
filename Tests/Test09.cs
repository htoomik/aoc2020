using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test09 : TestBase
    {
        public Test09(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            var input = @"
35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576".ChopToList();
            var solver = new Day09();
            var result = solver.Solve(input, 5);
            result.ShouldBe(127);
        }

        [Fact]
        public void Part2()
        {
            var input = @"
35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576".ChopToList();
            var solver = new Day09();
            var result = solver.Solve2(input, 127);
            result.ShouldBe(62);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.GetAllRows(9);
            var solver = new Day09();
            var result = solver.Solve(input, 25);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.GetAllRows(9);
            var solver = new Day09();
            var result = solver.Solve2(input, 104054607);
            Output.WriteLine(result.ToString());
        }
    }
}