using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test12 : TestBase
    {
        public Test12(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            var input = @"
F10
N3
F7
R90
F11".ChopToList();
            var solver = new Day12();
            var result = solver.Solve(input);
            result.ShouldBe(25);
        }

        [Fact]
        public void Part2()
        {
            var input = @"
F10
N3
F7
R90
F11".ChopToList();
            var solver = new Day12Part2();
            var result = solver.Solve(input);
            result.ShouldBe(286);
        }

        [Fact]
        public void Part2B()
        {
            var input = @"
F10
L180
F10".ChopToList();
            var solver = new Day12Part2();
            var result = solver.Solve(input);
            result.ShouldBe(0);
        }

        [Theory]
        [InlineData(10,-1,90,1,10)]
        [InlineData(10,-1,180,-10,1)]
        [InlineData(10,-1,270,-1,-10)]
        [InlineData(10,-1,360,10,-1)]
        public void Rotate(int x, int y, int degrees, int newX, int newY)
        {
            var waypoint = new Day12Part2.Waypoint { X = x, Y = y };
            waypoint.Rotate(degrees);
            waypoint.X.ShouldBe(newX);
            waypoint.Y.ShouldBe(newY);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.GetAllRows(12);
            var solver = new Day12();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.GetAllRows(12);
            var solver = new Day12Part2();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());

            // Fel: 39901
        }
    }
}