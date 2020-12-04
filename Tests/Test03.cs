using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test03 : TestBase
    {
        public Test03(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            var map = @"
..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#".ChopToList();

            var solver = new Day03();
            var result = solver.Solve(map, 3, 1);

            result.ShouldBe(7);
        }

        [Fact]
        public void Solve()
        {
            var map = DataHelper.GetAllRows(3);
            var solver = new Day03();
            var result = solver.Solve(map, 3, 1);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Part2()
        {
            var map = @"
..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#".ChopToList();

            var solver = new Day03();
            var result = solver.Solve2(map);
            result.ShouldBe(336);
        }

        [Fact]
        public void Solve2()
        {
            var map = DataHelper.GetAllRows(3);
            var solver = new Day03();
            var result = solver.Solve2(map);
            Output.WriteLine(result.ToString());
        }
    }
}