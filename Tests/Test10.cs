using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test10 : TestBase
    {
        public Test10(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            var input = @"
16
10
15
5
1
11
7
19
6
12
4".ChopToList();
            var solver = new Day10();
            var result = solver.Solve(input);
            result.ShouldBe(35);
        }

        [Fact]
        public void Part2()
        {
            var input = @"
16
10
15
5
1
11
7
19
6
12
4".ChopToList();
            var solver = new Day10();
            var result = solver.Solve2(input);
            result.ShouldBe(8);
        }

        [Fact]
        public void Part2B()
        {
            var input = @"
28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3".ChopToList();
            var solver = new Day10();
            var result = solver.Solve2(input);
            result.ShouldBe(19208);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.GetAllRows(10);
            var solver = new Day10();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.GetAllRows(10);
            var solver = new Day10();
            var result = solver.Solve2(input);
            Output.WriteLine(result.ToString());
        }
    }
}