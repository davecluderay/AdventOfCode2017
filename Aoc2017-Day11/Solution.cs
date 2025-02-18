using ArgumentException = System.ArgumentException;

namespace Aoc2017_Day11;

internal class Solution
{
    public string Title => "Day 11: Hex Ed";

    public object PartOne()
    {
        var moves = ReadMoves();

        var (x, y) = (0, 0);
        foreach (var move in moves)
        {
            (x, y) = Apply((x, y), move);
        }

        return CalculateStepsFromOrigin((x, y));
    }

    public object PartTwo()
    {
        var moves = ReadMoves();

        var (x, y) = (0, 0);
        var maximumSteps = 0;
        foreach (var move in moves)
        {
            (x, y) = Apply((x, y), move);
            maximumSteps = Math.Max(maximumSteps, CalculateStepsFromOrigin((x, y)));
        }

        return maximumSteps;
    }

    private static (int X, int Y) Apply((int X, int Y) at, string move)
    {
        var y = move switch
                {
                    "nw" or "ne" => at.Y - 1,
                    "n"          => at.Y - 2,
                    "sw" or "se" => at.Y + 1,
                    "s"          => at.Y + 2,
                    _            => throw new ArgumentException($"Unknown move: {move}", nameof(move))
                };
        var x = move switch
                {
                    "nw" or "sw" => at.X - 1,
                    "ne" or "se" => at.X + 1,
                    _            => at.X
                };
        return (x, y);
    }

    private static int CalculateStepsFromOrigin((int X, int Y) at)
    {
        var lateralSteps = Math.Abs(at.X);
        var verticalSteps = Math.Abs(at.Y) > Math.Abs(at.X)
                                ? (Math.Abs(at.Y) - Math.Abs(at.X)) / 2
                                : 0;
        return lateralSteps + verticalSteps;
    }

    private static string[] ReadMoves()
        => InputFile.ReadAllText()
                    .TrimEnd()
                    .Split(',')
                    .ToArray();
}
