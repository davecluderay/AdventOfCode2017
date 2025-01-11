using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2017_Day02;

internal partial class Solution
{
    public string Title => "Day 2: Corruption Checksum";

    public object PartOne()
        => ReadSpreadsheet().Sum(FindChecksum);

    public object PartTwo()
        => ReadSpreadsheet().Sum(FindResult);

    private static int FindChecksum(int[] row)
    {
        return row.Max() - row.Min();
    }

    private static int FindResult(int[] row)
    {
        row = row.ToArray();
        Array.Sort(row);
        for (var i = 0; i < row.Length - 1; i++)
        for (var j = i + 1; j < row.Length; j++)
        {
            if (row[j] % row[i] == 0)
            {
                return row[j] / row[i];
            }
        }
        throw new UnreachableException();
    }

    private static IEnumerable<int[]> ReadSpreadsheet()
        => InputFile.ReadAllLines().Select(line => SeparatorPattern.Split(line)
                                                                   .Select(int.Parse)
                                                                   .ToArray());

    [GeneratedRegex(@"\s+")]
    private static partial Regex SeparatorPattern { get; }
}
