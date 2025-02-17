using System.Text.RegularExpressions;

namespace Aoc2017_Day20;

internal partial record Particle(int Id, Vector Position, Vector Velocity, Vector Acceleration)
{
    public static Particle[] ReadAll()
        => InputFile.ReadAllLines()
                    .Select((line, id) => Parse(id, line))
                    .ToArray();

    public Vector CalculatePosition(long time)
    {
        return Position + time * Velocity + time * (time + 1) * Acceleration / 2;
    }

    private static Particle Parse(int id, string text)
    {
        var match = Pattern.Match(text);
        if (!match.Success) throw new FormatException($"Invalid input format: {text}");

        Vector position = (long.Parse(match.Groups["PX"].Value), long.Parse(match.Groups["PY"].Value), long.Parse(match.Groups["PZ"].Value));
        Vector velocity = (long.Parse(match.Groups["VX"].Value), long.Parse(match.Groups["VY"].Value), long.Parse(match.Groups["VZ"].Value));
        Vector acceleration = (long.Parse(match.Groups["AX"].Value), long.Parse(match.Groups["AY"].Value), long.Parse(match.Groups["AZ"].Value));
        return new(id, position, velocity, acceleration);
    }

    [GeneratedRegex(@"p=<(?<PX>[-\s]?\d+),(?<PY>[-\s]?\d+),(?<PZ>[-\s]?\d+)>,\s+v=<(?<VX>[-\s]?\d+),(?<VY>[-\s]?\d+),(?<VZ>[-\s]?\d+)>,\s+a=<(?<AX>[-\s]?\d+),(?<AY>[-\s]?\d+),(?<AZ>[-\s]?\d+)>")]
    private static partial Regex Pattern { get; }
}
