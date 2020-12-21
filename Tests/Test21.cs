using aoc2020.Code;
using aoc2020.Helpers;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test21 : TestBase
    {
        public Test21(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void Part1()
        {
            var input = @"
mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)".ChopToList();
            var solver = new Day21();
            var result = solver.Solve(input);
            result.ShouldBe(5);
        }

        [Fact]
        public void Part2()
        {
            var input = @"
mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)".ChopToList();
            var solver = new Day21();
            var result = solver.Solve2(input);
            result.ShouldBe("mxmxvkd,sqjhc,fvjkl");
        }

        [Fact]
        public void Solve()
        {
            var input = DataHelper.GetAllRows(21);
            var solver = new Day21();
            var result = solver.Solve(input);
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var input = DataHelper.GetAllRows(21);
            var solver = new Day21();
            var result = solver.Solve2(input);
            Output.WriteLine(result);
        }
    }
}