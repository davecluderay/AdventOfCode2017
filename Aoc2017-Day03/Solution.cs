using System.Diagnostics;

namespace Aoc2017_Day03;

internal class Solution
{
    public string Title => "Day 3: Spiral Memory";

    public object PartOne()
    {
        var address = ReadInputNumber();
        var location = SpiralMemory.GetLocation(address);
        return location.Position.ManhattanDistanceFrom((0, 0));
    }

    public object PartTwo()
    {
        var threshold = ReadInputNumber();
        Dictionary<Position, int> values = new() { [(0, 0)] = 1 };
        foreach (var location in SpiralMemory.GetLocations().Skip(1))
        {
            var value = location.Position.GetAdjacentPositions()
                                         .Sum(p => values.GetValueOrDefault(p, 0));
            values[location.Position] = value;
            if (value > threshold)
            {
                return value;
            }
        }
        throw new UnreachableException();
    }

    private static int ReadInputNumber()
    {
        return int.Parse(InputFile.ReadAllText());
    }
}
