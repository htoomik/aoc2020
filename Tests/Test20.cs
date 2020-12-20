using System.IO;
using System.Linq;
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
        public void Part2()
        {
            var input = File.ReadAllText("C:\\Code\\aoc2020\\Data\\test20.txt");
            var solver = new Day20();
            var result = solver.Solve2(input);
            result.ShouldBe(273);
        }

        [Fact]
        public void Trim()
        {
            var original = @"
#...##.#..
..#.#..#.#
.###....#.
###.##.##.
.###.#####
.##.#....#
#...######
.....#..##
#.####...#
#.##...##.".Trim();

            var expected = @"
.#.#..#.
###....#
##.##.##
###.####
##.#....
...#####
....#..#
.####...".Trim();

            var data = original.ChopToList().Select(row => row.ToCharArray()).ToArray();
            var tile = new Day20.Tile(0, data);
            var trimmed = Day20.Trim(tile);
            var asString = Day20.DataToString(trimmed.Data).Trim();
            asString.ShouldBe(expected);
        }

        [Fact]
        public void Flip()
        {
            var original = @"
#....####.
#..#.##...
#.##..#...
######.#.#
.#...#.#.#
.#########
.###.#..#.
########.#
##...##.#.
..###.#.#.".Trim();

            var expected = @"
..###.#.#.
##...##.#.
########.#
.###.#..#.
.#########
.#...#.#.#
######.#.#
#.##..#...
#..#.##...
#....####.".Trim();

            var input = original.ChopToList().Select(row => row.ToCharArray().ToList()).ToList();
            var result = Day20.Flip(input);
            var asString = Day20.DataToString(result);
            asString.ShouldBe(expected, "result");

            var inputAsString = Day20.DataToString(input);
            inputAsString.ShouldBe(original, "original");
        }


        [Fact]
        public void Rotate()
        {
            var original = @"
123
456
789".Trim();
            var expected = @"
741
852
963".Trim();
            var input = original.ChopToList().Select(row => row.ToCharArray().ToList()).ToList();
            var result = Day20.Rotate(input);
            var asString = Day20.DataToString(result);
            asString.ShouldBe(expected, "result");

            var inputAsString = Day20.DataToString(input);
            inputAsString.ShouldBe(original, "original");
        }

        [Fact]
        public void RotateAndFlipMini()
        {
            var original = @"
123
456
789".Trim();

            var exFlipped = @"
789
456
123".Trim();

            var expected = @"
147
258
369".Trim();

            var input = original.ChopToList().Select(row => row.ToCharArray().ToList()).ToList();
            var flipped = Day20.Flip(input);
            var rotated = Day20.Rotate(flipped);

            var flippedAsString = Day20.DataToString(flipped);
            flippedAsString.ShouldBe(exFlipped, "exFlipped");

            var rotatedAsString = Day20.DataToString(rotated);
            rotatedAsString.ShouldBe(expected, "expected");
        }

        [Fact]
        public void RotateAndFlip()
        {
            var original = @"
#....####.
#..#.##...
#.##..#...
######.#.#
.#...#.#.#
.#########
.###.#..#.
########.#
##...##.#.
..###.#.#.".Trim();

            var expected = @"
..#.###...
##.##....#
..#.###..#
###.#..###
.######.##
#.#.#.#...
#.###.###.
#.###.##..
.######...
.##...####".Trim();

            var input = original.ChopToList().Select(row => row.ToCharArray().ToList()).ToList();
            var r1 = Day20.Flip(Day20.Rotate(input));
            var asString = Day20.DataToString(r1);
            asString.ShouldBe(expected);
        }

        [Fact]
        public void TransformSo()
        {
            var original = @"
#....####.
#..#.##...
#.##..#...
######.#.#
.#...#.#.#
.#########
.###.#..#.
########.#
##...##.#.
..###.#.#.".Trim();

            var expected = @"
..#.###...
##.##....#
..#.###..#
###.#..###
.######.##
#.#.#.#...
#.###.###.
#.###.##..
.######...
.##...####".Trim();

            var input = original.ChopToList().Select(row => row.ToCharArray()).ToArray();
            var asTile = new Day20.Tile(0, input);
            var r1 = Day20.TransformSo(asTile, "..#.###...", Day20.GetTop);
            var asString = Day20.DataToString(r1.Data);
            asString.ShouldBe(expected);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.Get(20);
            var solver = new Day20();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.Get(20);
            var solver = new Day20();
            var result = solver.Solve2(input);
            Output.WriteLine(result.ToString());
        }
    }
}