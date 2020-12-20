using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc2020.Code
{
    public class Day20
    {
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

        public long Solve2(string input)
        {
            var tiles = Parse(input);
            var map = Puzzle(tiles);
            var trimmedTiles = tiles.Select(Trim).ToList();
            var image = Assemble(map, trimmedTiles);

            var seaMonsters = DetectMonsters(image);

            return map.Count;
        }

        private char[,] Assemble(List<List<int>> map, List<Tile> tiles)
        {
            var tileWidth = tiles[0].Data[0].Length;
            var tileHeight = tiles[0].Data.Length;

            var fullWidth = tileWidth * map[0].Count();
            var fullHeight = tileHeight * map.Count();
            var image = new char [fullHeight, fullWidth];

            var tileDict = tiles.ToDictionary(t => t.Id, t => t);

            for (var ty = 0; ty < map.Count; ty++)
            {
                var mapRow = map[ty];
                for (var tx = 0; tx < mapRow.Count; tx++)
                {
                    var tileId = mapRow[tx];
                    var tile = tileDict[tileId];
                    var xStart = tx * tileWidth;
                    var yStart = ty * tileHeight;
                    for (int x = 0; x < tileWidth; x++)
                    {
                        for (int y = 0; y < tileHeight; y++)
                        {
                            var fullX = xStart + x;
                            var fullY = yStart + y;

                            image[fullY, fullX] = tile.Data[y][x];
                        }
                    }
                }
            }

            return image;
        }

        private Tile Trim(Tile input)
        {
            var id = input.Id;
            var data = input.Data;
            var newData = data[1..^2].Select(row => row[1..^2]).ToArray();
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
            while (true)
            {
                var next = unused.First(t => matches.Contains(new Tuple<int, int>(current, t)) && matchesPerTile[t] < 4);

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
                var first = unused.First(t => matches.Contains(new Tuple<int, int>(t, previousLeftEdge)));
                map[rowIndex].Add(first);

                // Starting last row?
                var edgeMatchCount = unused.Count() == map[0].Count() ? 2 : 3;

                // The rest
                current = first;
                while (true)
                {
                    var next = unused.First(t => matches.Contains(new Tuple<int, int>(current, t)));

                    map[rowIndex].Add(next);
                    current = next;
                    unused.Remove(next);

                    // Reached right edge
                    if (matchesPerTile[next] == edgeMatchCount)
                    {
                        rowIndex++;
                        break;
                    }
                }

                if (edgeMatchCount == 2)
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

        private class Tile
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
                    side1, side2, side3, side4,
                    side5, side6, side7, side8
                };

                return sides;
            }
        }
    }
}