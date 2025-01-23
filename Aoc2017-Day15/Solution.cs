namespace Aoc2017_Day15;

internal class Solution
{
    public string Title => "Day 15: Dueling Generators";

    public object PartOne()
    {
        var (a, b) = ReadInitialGeneratorValues();
        var matches = 0;
        for (var n = 0; n < 40_000_000; n++)
        {
            a = Next(a,16807L);
            b = Next(b, 48271L);
            if (IsMatch(a, b)) matches++;
        }
        return matches;

        static long Next(long previous, long factor)
        {
            return (previous * factor) % 2147483647L;
        }
    }

    public object PartTwo()
    {
        var (a, b) = ReadInitialGeneratorValues();
        var matches = 0;
        for (var n = 0; n < 5_000_000; n++)
        {
            a = Next(a, 16807L, 0b11);
            b = Next(b, 48271L, 0b111);
            if (IsMatch(a, b)) matches++;
        }
        return matches;

        static long Next(long value, long factor, long mask)
        {
            do { value = (value * factor) % 2147483647L; }
            while ((value & mask) != 0);
            return value;
        }
    }

    private static bool IsMatch(long a, long b)
    {
        return (a & 0xFFFF) == (b & 0xFFFF);
    }

    private static (long A, long B) ReadInitialGeneratorValues()
    {
        var values = InputFile.ReadAllLines()
                              .Select(l => l.Substring(l.LastIndexOf(' ') + 1))
                              .Select(long.Parse)
                              .ToArray();
        return values switch
               {
                   [var a, var b] => (a, b),
                   _              => throw new InvalidOperationException($"Unexpected data: {string.Join(", ", values)}")
               };
    }
}
