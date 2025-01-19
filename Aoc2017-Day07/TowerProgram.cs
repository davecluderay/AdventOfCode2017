using System.Text.RegularExpressions;

namespace Aoc2017_Day07;

internal readonly partial record struct TowerProgram(string Name, int Weight, string[] Dependants)
{
    public static TowerProgram[] ReadAll()
        => InputFile.ReadAllLines().Select(Parse).ToArray();

    private static TowerProgram Parse(string line)
    {
        var match = Pattern.Match(line);
        if (!match.Success) throw new FormatException("Unexpected format: " + line);
        var name = match.Groups["Name"].Value;
        var weight = int.Parse(match.Groups["Weight"].Value);
        var dependants = match.Groups["Dependants"].Captures.Select(c => c.Value).ToArray();
        return new TowerProgram(name, weight, dependants);
    }

    [GeneratedRegex(@"^(?<Name>\w+)\s+\((?<Weight>\d+)\)(\s+->\s+(?<Dependants>\w+)(,\s+(?<Dependants>\w+))*)?$")]
    private static partial Regex Pattern { get; }
}
