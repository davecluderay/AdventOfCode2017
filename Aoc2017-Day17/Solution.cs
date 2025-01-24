using System.Diagnostics;

namespace Aoc2017_Day17;

internal class Solution
{
    public string Title => "Day 17: Spinlock";

    public object PartOne()
    {
        var skip = ReadSkipCount();

        var at = 0;
        var buffer = new List<int>(2018) { 0 };
        for (var i = 1; i <= 2017; i++)
        {
            at = (at + skip) % buffer.Count;
            buffer.Insert(++at, i);
        }

        return buffer[(at + 1) % buffer.Count];
    }

    public object PartTwo()
    {
        var skip = ReadSkipCount();

        var at = 0;
        var valueAfterZero = 0;
        for (var i = 1; i <= 50_000_000; i++)
        {
            at = (at + skip) % i + 1;
            if (at == 1)
            {
                valueAfterZero = i;
            }
        }

        return valueAfterZero;
    }

    private static int ReadSkipCount()
        => int.Parse(InputFile.ReadAllText().TrimEnd());
}
