using Xunit.Abstractions;

namespace aoc2020.Tests
{
    public class TestBase
    {
        protected ITestOutputHelper Output { get; }

        protected TestBase(ITestOutputHelper helper)
        {
            Output = helper;
        }
    }
}