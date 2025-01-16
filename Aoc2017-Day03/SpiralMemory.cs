namespace Aoc2017_Day03;

internal static class SpiralMemory
{
    public static MemoryLocation GetLocation(int address)
    {
        return GetLocations().First(l => l.Address == address);
    }

    public static IEnumerable<MemoryLocation> GetLocations()
    {
        Position position = (0, 0);
        Vector direction = (1, 0);
        var address = 1;
        var stepsTaken = 0;
        using var segments = GetSegmentLengths().GetEnumerator();
        segments.MoveNext();
        while (true)
        {
            yield return new MemoryLocation(address, position);
            position += direction;
            address++;
            stepsTaken++;

            if (stepsTaken == segments.Current)
            {
                stepsTaken = 0;
                direction = direction.RotateLeft();
                segments.MoveNext();
            }
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private static IEnumerable<int> GetSegmentLengths()
    {
        var n = 1;
        while (true)
        {
            yield return n;
            yield return n;
            n++;
        }
        // ReSharper disable once IteratorNeverReturns
    }
}
