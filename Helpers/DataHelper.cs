using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc2020.Helpers
{
    public static class DataHelper
    {
        public static List<string> GetAllRows(int day)
        {
            var lines = File.ReadAllLines($"C:\\Code\\aoc2020\\Data\\input{day:00}.txt");
            return lines.ToList();
        }

        public static List<string> ChopToList(this string data)
        {
            return data.Trim().Split("\r\n").ToList();
        }
    }
}