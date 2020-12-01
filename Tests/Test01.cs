using System.Collections.Generic;
using aoc2020.Code;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class Test01
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test01(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Fact01()
        {
            var data = new List<string>
            {
                "1721",
                "979",
                "366",
                "299",
                "675",
                "1456",
            };
            var sut = new Day01(data);
            var result = sut.Solve();
            result.ShouldBe(514579);
        }

        [Fact]
        public void Part1()
        {
            var sut = new Day01();
            var result = sut.Solve();

            _testOutputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public void Part2()
        {
            var sut = new Day01();
            var result = sut.Solve2();

            _testOutputHelper.WriteLine(result.ToString());
        }
    }
}