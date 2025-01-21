using System.Text.RegularExpressions;

namespace Aoc2017_Day13;

internal readonly partial record struct Scanner(int Depth, int Range)
{
    public bool WillDetectAt(int picoseconds)
    {
        var n = picoseconds % (Range * 2 - 2);
        var position = n < Range
                           ? n
                           : Range * 2 - n - 1;
        return position == 0;
    }

    public int DetectionSeverity => Depth * Range;

    public static Scanner Parse(string text)
    {
        var match = Pattern.Match(text);
        if (!match.Success) throw new ArgumentException($"Unrecognised format: {text}", nameof(text));

        return new(int.Parse(match.Groups["Depth"].Value),
                   int.Parse(match.Groups["Range"].Value));
    }

    [GeneratedRegex(@"^(?<Depth>\d+):\s*(?<Range>\d+)$")]
    private static partial Regex Pattern { get; }
}
