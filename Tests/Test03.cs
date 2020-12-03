using System;
using System.Collections.Generic;
using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test03
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test03(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
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
            _testOutputHelper.WriteLine(result.ToString());
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
            _testOutputHelper.WriteLine(result.ToString());
        }
    }
}