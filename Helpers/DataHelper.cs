using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc2020.Helpers
{
    public class DataHelper
    {
        public static List<string> GetAllRows(int day)
        {
            var lines = File.ReadAllLines($"C:\\Code\\aoc2020\\Data\\input{day:00}.txt");
            return lines.ToList();
        }
    }
}