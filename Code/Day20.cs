using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using aoc2020.Helpers;

namespace aoc2020.Code
{
    public class Day20
    {
        private static readonly List<List<char>> Monster = InitializeMonster();

        private static List<List<char>> InitializeMonster()
        {
            const string monster = @"
                  # 
#    ##    ##    ###
 #  #  #  #  #  #   ";
            return monster.ChopToList().Skip(1).Select(line => line.ToCharArray().ToList()).ToList();
        }

        public long Solve(string input)
        {
            var tiles = Parse(input);
            var matches = GetMatches(tiles);
            var matchesPerTile = GetMatchesPerTile(tiles, matches);
            var corners = FindCorners(matchesPerTile);

            if (corners.Count != 4)
            {
                throw new Exception();
            }

            return corners.Aggregate(1L, (current, corner) => current * corner);
        }

        public int Solve2(string input)
        {
            var tiles = Parse(input);

            WriteToFile("raw 1951", tiles.Single(t => t.Id == 1951).Data);

            var map = Puzzle(tiles);

            var rotatedTiles = FitPieces(tiles, map);

            WriteToFile("rotated 1951", rotatedTiles.Single(t => t.Id == 1951).Data);

            var trimmedTiles = rotatedTiles.Select(Trim).ToList();

            WriteToFile("trimmed 1951", trimmedTiles.Single(t => t.Id == 1951).Data);

            var image = Assemble(map, trimmedTiles);

            WriteToFile("original", image);

            var transforms = GetTransforms(image);

            for (var index = 0; index < transforms.Count; index++)
            {
                var transform = transforms[index];
                WriteToFile(index.ToString(), transform);
                var (foundMonsters, markedMap) = FindMonsters(transform);
                if (foundMonsters)
                {
                    var nonMonsters = CountNonMonsters(markedMap);
                    return nonMonsters;
                }
            }

            throw new Exception();
        }

        private List<Tile> FitPieces(List<Tile> tiles, List<List<int>> map)
        {
            var firstId = map[0][0];
            var secondId = map[0][1];
            var belowId = map[1][0];
            var tileDict = tiles.ToDictionary(t => t.Id, t => t);
            var first = tileDict[firstId];
            var second = tileDict[secondId];
            var below = tileDict[belowId];

            var matchesBetweenFirstAndSecond = first.Sides.Where(s => second.Sides.Any(s2 => s == s2)).ToList();

            if (matchesBetweenFirstAndSecond.Count != 2)
            {
                var s = string.Join(",", matchesBetweenFirstAndSecond);
                throw new Exception(s);
            }

            var matchingSide1 = matchesBetweenFirstAndSecond[0];
            var fittedFirst = TransformSoRightSideIs(first, matchingSide1);

            var matchesBetweenFirstAndBelow = below.Sides.Where(s => s == GetBottom(fittedFirst.Data)).ToList();
            if (matchesBetweenFirstAndBelow.Count == 0)
            {
                fittedFirst = new Tile(firstId, ToArray(Flip(ToList(fittedFirst.Data))));
            }
            matchesBetweenFirstAndBelow = below.Sides.Where(s => s == GetBottom(fittedFirst.Data)).ToList();
            if (matchesBetweenFirstAndBelow.Count != 1)
            {
                throw new Exception();
            }

            var fittedTiles = new Dictionary<int, Tile> { { fittedFirst.Id, fittedFirst } };

            for (var y = 0; y < map.Count; y++)
            {
                var row = map[y];

                if (y != 0)
                {
                    var firstIdInRow = row[0];
                    var firstTileInRow = tileDict[firstIdInRow];
                    var previousTileId = map[y - 1][0];
                    var previousTile = fittedTiles[previousTileId];
                    var sideToMatch = GetBottom(previousTile.Data);
                    var fittedTile = TransformSoTopIs(firstTileInRow, sideToMatch);
                    fittedTiles[fittedTile.Id] = fittedTile;
                }

                for (var x = 1; x < row.Count; x++)
                {
                    var tile = tileDict[row[x]];
                    var previousTile = fittedTiles[row[x - 1]];
                    var sideToMatch = GetRightSide(previousTile.Data);
                    var fittedTile = TransformSoLeftSideIs(tile, sideToMatch);
                    fittedTiles[fittedTile.Id] = fittedTile;
                }
            }

            return fittedTiles.Values.ToList();
        }

        private Tile TransformSoRightSideIs(Tile tile, string side)
        {
            return TransformSo(tile, side, GetRightSide);
        }

        private Tile TransformSoTopIs(Tile tile, string side)
        {
            return TransformSo(tile, side, GetTop);
        }

        private Tile TransformSoLeftSideIs(Tile tile, string side)
        {
            return TransformSo(tile, side, GetLeftSide);
        }

        public static Tile TransformSo(Tile tile, string toMatch, Func<char[][], string> matchFunction)
        {
            var original = tile.Data;

            var side = matchFunction(original);
            if (side == toMatch)
            {
                return tile;
            }

            var rotated = original.Select(row => row.ToList()).ToList();
            for (var i = 0; i < 3; i++)
            {
                rotated = Rotate(rotated);
                var asArray = ToArray(rotated);
                side = matchFunction(asArray);
                if (side == toMatch)
                {
                    return new Tile(tile.Id, asArray);
                }
            }

            var flipped = Flip(original.Select(row => row.ToList()).ToList());
            var flippedAsArray = ToArray(flipped);
            var rightSide2 = matchFunction(flippedAsArray);
            if (rightSide2 == toMatch)
            {
                return new Tile(tile.Id, flippedAsArray);
            }

            rotated = flipped;
            for (var i = 0; i < 3; i++)
            {
                rotated = Rotate(rotated);
                var asArray = ToArray(rotated);
                side = matchFunction(asArray);
                if (side == toMatch)
                {
                    return new Tile(tile.Id, asArray);
                }
            }

            throw new Exception();
        }

        private static char[][] ToArray(List<List<char>> rotated)
        {
            return rotated.Select(row => row.ToArray()).ToArray();
        }

        private static List<List<char>> ToList(  char[][] rotated)
        {
            return rotated.Select(row => row.ToList()).ToList();
        }

        private static void WriteToFile(string name, IEnumerable<IEnumerable<char>> transform)
        {
            var s = DataToString(transform);
            File.WriteAllText($"C:\\Code\\aoc2020\\Log\\{name}.txt", s);
        }

        public static string DataToString(IEnumerable<IEnumerable<char>> transform)
        {
            var sb = new StringBuilder();
            foreach (var row in transform)
            {
                foreach (var c in row)
                {
                    sb.Append(c);
                }

                sb.AppendLine();
            }

            var s = sb.ToString().Trim();
            return s;
        }

        private int CountNonMonsters(List<List<char>> markedMap)
        {
            return markedMap.Sum(row => row.Count(c => c == 'O'));
        }

        private (bool found, List<List<char>> markedMap) FindMonsters(List<List<char>> image)
        {
            var height = Monster.Count;
            var width = Monster[0].Count;
            var copy = image.Select(row => row.Select(c => c).ToList()).ToList();
            var found = false;

            for (var y = 0; y < image.Count - height + 1; y++)
            {
                for (var x = 0; x < image[0].Count - width + 1; x++)
                {
                    var isMonster = true;
                    for (var my = 0; my < height; my++)
                    {
                        for (var mx = 0; mx < width; mx++)
                        {
                            var mc = Monster[my][mx];
                            if (mc == ' ')
                            {
                                continue;
                            }

                            var ic = image[y + my][x + mx];
                            if (ic == '.')
                            {
                                isMonster = false;
                                break;
                            }
                        }

                        if (!isMonster)
                        {
                            break;
                        }
                    }

                    if (isMonster)
                    {
                        found = true;
                        for (var my = 0; my < height; my++)
                        {
                            for (var mx = 0; mx < width; mx++)
                            {
                                var mc = Monster[y][x];
                                if (mc == '#')
                                {
                                    copy[y][x] = 'O';
                                }
                            }
                        }
                    }
                }
            }

            return found
                ? (true, copy)
                : (false, image);
        }

        private List<List<List<char>>> GetTransforms(List<List<char>> original)
        {
            var flipped = Flip(original);
            var results = new List<List<List<char>>> { original, flipped };

            for (var i = 0; i < 3; i++)
            {
                var rotated = Rotate(original);
                var rotatedFlipped = Rotate(flipped);
                results.Add(rotated);
                results.Add(rotatedFlipped);
            }

            return results;
        }

        public static List<List<char>> Flip(List<List<char>> original)
        {
            return original.Select(o => o.ToList()).Reverse().ToList();
        }

        public static List<List<char>> Rotate(List<List<char>> original)
        {
            var result = new List<List<char>>();

            var width = original[0].Count;
            for (var i = 0; i < width; i++)
            {
                result.Add(new List<char>());
            }

            for (var index = original.Count - 1; index >= 0; index--)
            {
                var row = original[index];
                for (var j = 0; j < original[0].Count; j++)
                {
                    result[j].Add(row[j]);
                }
            }

            return result;
        }

        private List<List<char>> Assemble(List<List<int>> map, List<Tile> tiles)
        {
            var tileWidth = tiles[0].Data[0].Length;
            var tileHeight = tiles[0].Data.Length;

            var image = new List<List<char>>();
            var tileDict = tiles.ToDictionary(t => t.Id, t => t);

            for (var ty = 0; ty < map.Count; ty++)
            {
                image.Add(new List<char>());
                var mapRow = map[ty];
                foreach (var tileId in mapRow)
                {
                    var tile = tileDict[tileId];
                    var yStart = ty * tileHeight;
                    for (var x = 0; x < tileWidth; x++)
                    {
                        for (var y = 0; y < tileHeight; y++)
                        {
                            var fullY = yStart + y;

                            if (image.Count <= fullY)
                            {
                                image.Add(new List<char>());
                            }

                            image[fullY].Add(tile.Data[y][x]);
                        }
                    }
                }
            }

            return image;
        }

        public static Tile Trim(Tile input)
        {
            var id = input.Id;
            var data = input.Data;
            var newData = data[1..^1].Select(row => row[1..^1]).ToArray();
            return new Tile(id, newData);
        }

        private List<List<int>> Puzzle(List<Tile> tiles)
        {
            var matches = GetMatches(tiles);
            var matchesPerTile = GetMatchesPerTile(tiles, matches);
            var corners = FindCorners(matchesPerTile);

            var tileIds = tiles.Select(t => t.Id).ToList();
            var unused = tileIds.Select(m => m).ToList();
            var corner = corners[0];
            var map = new List<List<int>> { new List<int> { corner } };

            // Top
            var current = corner;
            unused.Remove(corner);
            while (true)
            {
                var next = unused.First(t =>
                    (matches.Contains(new Tuple<int, int>(current, t)) ||
                     matches.Contains(new Tuple<int, int>(t, current))) &&
                    matchesPerTile[t] < 4);

                map[0].Add(next);
                current = next;
                unused.Remove(next);

                // Reached top right corner
                if (matchesPerTile[next] == 2)
                {
                    break;
                }
            }

            // Rows
            var rowIndex = 1;
            while (true)
            {
                map.Add(new List<int>());

                // The one above to match
                var previousLeftEdge = map[rowIndex - 1].First();

                // Left edge
                var first = unused.First(t =>
                    matches.Contains(new Tuple<int, int>(t, previousLeftEdge)) ||
                    matches.Contains(new Tuple<int, int>(previousLeftEdge, t)));
                map[rowIndex].Add(first);
                unused.Remove(first);

                // The rest
                current = first;
                var x = 1;
                while (true)
                {
                    var above = map[rowIndex - 1][x];
                    var next = unused.First
                    (t =>
                        (matches.Contains(new Tuple<int, int>(current, t)) ||
                         matches.Contains(new Tuple<int, int>(t, current)))
                        &&
                        (matches.Contains(new Tuple<int, int>(above, t)) ||
                         matches.Contains(new Tuple<int, int>(t, above)))
                    );

                    map[rowIndex].Add(next);
                    current = next;
                    unused.Remove(next);

                    x++;

                    // Reached right edge
                    if (x == map[0].Count)
                    {
                        rowIndex++;
                        break;
                    }
                }

                if (!unused.Any())
                {
                    break;
                }
            }

            return map;
        }

        private static List<int> FindCorners(Dictionary<int, int> matchesPerTile)
        {
            return matchesPerTile.Where(kvp => kvp.Value == 2).Select(kvp => kvp.Key).ToList();
        }

        private Dictionary<int, int> GetMatchesPerTile(List<Tile> tiles, HashSet<Tuple<int, int>> matches)
        {
            return tiles.ToDictionary(
                t => t.Id,
                t => matches.Count(m => m.Item1 == t.Id || m.Item2 == t.Id));
        }

        private HashSet<Tuple<int, int>> GetMatches(List<Tile> tiles)
        {
            var matches = new HashSet<Tuple<int, int>>();

            for (var i = 0; i < tiles.Count; i++)
            {
                for (var j = i + 1; j < tiles.Count; j++)
                {
                    var tile1 = tiles[i];
                    var tile2 = tiles[j];
                    if (Matches(tile1, tile2))
                    {
                        matches.Add(new Tuple<int, int>(tile1.Id, tile2.Id));
                    }
                }
            }

            return matches;
        }

        private List<Tile> Parse(string input)
        {
            var cleaned = input.Replace("\r\n", "\n").Trim();
            var chunks = cleaned.Split("\n\n");
            var tiles = chunks.Select(ParseTile).ToList();
            return tiles;
        }

        private Tile ParseTile(string input)
        {
            var lines = input.Split("\n");
            var id = int.Parse(lines[0].Replace("Tile ", "").Replace(":", ""));
            var data = lines.Skip(1).Select(l => l.ToCharArray()).ToArray();
            return new Tile(id, data);
        }

        private bool Matches(Tile tile1, Tile tile2)
        {
            return tile1.Sides.Any(side => tile2.Sides.Any(side2 => side == side2));
        }

        public class Tile
        {
            public int Id { get; }
            public List<string> Sides { get; }
            public char[][] Data { get; }

            public Tile(in int id, char[][] data)
            {
                Id = id;
                Data = data;
                Sides = CalculateSides();
            }

            private List<string> CalculateSides()
            {
                var side1 = string.Join("", Data[0]);
                var side2 = string.Join("", Data[^1]);
                var side3 = string.Join("", Data.Select(d => d[0]));
                var side4 = string.Join("", Data.Select(d => d[^1]));

                var side5 = string.Join("", side1.Reverse());
                var side6 = string.Join("", side2.Reverse());
                var side7 = string.Join("", side3.Reverse());
                var side8 = string.Join("", side4.Reverse());

                var sides = new List<string>
                {
                    side1,
                    side2,
                    side3,
                    side4,
                    side5,
                    side6,
                    side7,
                    side8,
                };

                return sides;
            }
        }

        private static string GetRightSide(char[][] data)
        {
            return string.Join("", data.Select(d => d[^1]));
        }

        private static string GetLeftSide(char[][] data)
        {
            return string.Join("", data.Select(d => d[0]));
        }

        public static string GetTop(char[][] data)
        {
            return string.Join("", data[0]);
        }

        private static string GetBottom(char[][] data)
        {
            return string.Join("", data[^1]);
        }
    }
}