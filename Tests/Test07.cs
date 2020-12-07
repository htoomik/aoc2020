using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test07:TestBase
    {
        public Test07(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            var input = @"
light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.".ChopToList();
            var solver = new Day07();
            var result = solver.Solve(input);
            result.ShouldBe(4);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.GetAllRows(7);
            var solver = new Day07();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Part2()
        {
            var input = @"
light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.".ChopToList();
            var solver = new Day07();
            var result = solver.Solve2(input);
            result.ShouldBe(32);
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.GetAllRows(7);
            var solver = new Day07();
            var result = solver.Solve2(input);
            Output.WriteLine(result.ToString());
        }
    }
}