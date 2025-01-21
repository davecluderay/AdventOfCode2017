using System.Globalization;
using System.Numerics;

namespace Aoc2017_Day14;

internal class Solution
{
    public string Title => "Day 14: Disk Defragmentation";

    public object PartOne()
    {
        var key = InputFile.ReadAllText().Trim();
        var layout = CalculateDiskLayout(key);
        return CountUsedSquares(layout);
    }

    public object PartTwo()
    {
        var key = InputFile.ReadAllText().Trim();
        var layout = CalculateDiskLayout(key);
        return CountUsedRegions(layout);
    }

    private static string[] CalculateDiskLayout(string key)
    {
        var layout = new string[128];
        for (var row = 0; row < layout.Length; row++)
        {
            var hash = KnotHash.Calculate($"{key}-{row}");
            var n = BigInteger.Parse(hash, NumberStyles.HexNumber);
            layout[row] = $"{n:b128}".Replace('0', '.').Replace('1', '#');
        }
        return layout;
    }

    private static int CountUsedSquares(string[] layout)
    {
        var usedSquares = layout.SelectMany(s => s)
                                .Count(c => c == '#');
        return usedSquares;
    }

    private static int CountUsedRegions(string[] layout)
    {
        HashSet<Position> set = [];
        for (var y = 0; y < layout.Length; y++)
        for (var x = 0; x < layout[y].Length; x++)
        {
            if (layout[y][x] != '#') continue;
            set.Add((x, y));
        }

        var regionCount = 0;

        while (set.Count != 0)
        {
            regionCount++;

            var examine = new Stack<Position>([set.First()]);
            while (examine.Count != 0)
            {
                var current = examine.Pop();
                set.Remove(current);
                foreach (var adjacent in current.GetAdjacentPositions())
                {
                    if (set.Contains(adjacent)) examine.Push(adjacent);
                }
            }
        }

        return regionCount;
    }
}
