using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test08 : TestBase
    {
        public Test08(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            var input = @"
nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6".ChopToList();
            var solver = new Day08();
            var result = solver.Solve(input);
            result.ShouldBe(5);
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.GetAllRows(8);
            var solver = new Day08();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Part2()
        {
            var input = @"
nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6".ChopToList();
            var solver = new Day08();
            var result = solver.Solve2(input);
            result.ShouldBe(8);
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.GetAllRows(8);
            var solver = new Day08();
            var result = solver.Solve2(input);
            Output.WriteLine(result.ToString());
        }

    }
}