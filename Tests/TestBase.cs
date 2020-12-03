using Xunit.Abstractions;
using Xunit.Sdk;

namespace aoc2020.Tests
{
    public class TestBase
    {
        protected ITestOutputHelper Output { get; }

        protected TestBase()
        {
            Output = new TestOutputHelper();
        }
    }
}