namespace Aoc2017_Day13;

internal class Solution
{
    public string Title => "Day 13: Packet Scanners";

    public object PartOne()
    {
        var scanners = ReadScanners();
        var detectingScanners = scanners.Where(s => s.WillDetectAt(s.Depth))
                                        .ToArray();
        return detectingScanners.Sum(s => s.DetectionSeverity);
    }

    public object PartTwo()
    {
        var scanners = ReadScanners();
        var delay = 0;
        while (scanners.Any(s => s.WillDetectAt(s.Depth + delay)))
        {
            ++delay;
        }
        return delay;
    }

    private static Scanner[] ReadScanners()
    {
        return InputFile.ReadAllLines()
                        .Select(Scanner.Parse)
                        .ToArray();
    }
}
