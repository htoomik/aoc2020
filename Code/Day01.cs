using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using aoc2020.Helpers;

namespace aoc2020.Code
{
    public class Day01
    {
        private readonly List<string> _data;

        public Day01(List<string> data = null)
        {
            _data = data ?? DataHelper.GetAllRows(1);
        }

        public int Solve()
        {
            var nums = _data.Select(int.Parse).ToList();

            for (int i = 0; i < nums.Count(); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    var x = nums[i];
                    var y = nums[j];
                    if (x + y == 2020)
                        return x * y;
                }
            }

            return 0;
        }

        public int Solve2()
        {
            var nums = _data.Select(int.Parse).ToList();

            for (int i = 0; i < nums.Count(); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    for (int k = 0; k < j; k++)
                    {
                        var x = nums[i];
                        var y = nums[j];
                        var z = nums[k];
                        if (x + y + z == 2020)
                            return x * y * z;
                    }
                }
            }

            return 0;
        }
    }
}