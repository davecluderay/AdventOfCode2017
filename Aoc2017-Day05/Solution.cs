namespace Aoc2017_Day05;

internal class Solution
{
    public string Title => "Day 5: A Maze of Twisty Trampolines, All Alike";

    public object PartOne()
    {
        var offsets = ReadOffsets();
        var steps = CountStepsToReachExit(offsets, TransformOffsetStrange);
        return steps;
    }

    public object PartTwo()
    {
        var offsets = ReadOffsets();
        var steps = CountStepsToReachExit(offsets, TransformOffsetMoreStrange);
        return steps;
    }

    private static int CountStepsToReachExit(int[] offsets, Func<int, int> transformOffset)
    {
        offsets = offsets.ToArray();

        var index = 0;
        var steps = 0;
        while (index >= 0 && index < offsets.Length)
        {
            var offset = offsets[index];
            offsets[index] = transformOffset(offset);
            index += offset;
            steps++;
        }

        return steps;
    }

    private static int TransformOffsetStrange(int offset)
        => offset + 1;

    private static int TransformOffsetMoreStrange(int offset)
        => offset + (offset < 3 ? 1 : -1);

    private static int[] ReadOffsets()
        => InputFile.ReadAllLines()
                    .Select(int.Parse)
                    .ToArray();
}
