using System.Text.RegularExpressions;

namespace Aoc2017_Day12;

internal static partial class ConnectivityReport
{
    public static Dictionary<int, int[]> Read()
        => InputFile.ReadAllLines()
                    .Select(ParseReportLine)
                    .ToDictionary(r => r.Reporter,
                                  r => r.Connections);

    private static (int Reporter, int[] Connections) ParseReportLine(string text)
    {
        var match = ReportLinePattern.Match(text);
        if (!match.Success) throw new FormatException("Unexpected format: " + text);

        var reporter = int.Parse(match.Groups["Reporter"].Value);
        var connections = match.Groups["Connections"].Captures.Select(c => int.Parse(c.Value)).ToArray();

        return (reporter, connections);
    }

    [GeneratedRegex(@"^(?<Reporter>\d+)\s+<->\s+(?<Connections>\d+)(,\s+(?<Connections>\d+))*$")]
    private static partial Regex ReportLinePattern { get; }
}
