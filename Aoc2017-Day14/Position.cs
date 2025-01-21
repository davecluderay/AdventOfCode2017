namespace Aoc2017_Day14;

internal readonly record struct Position(int X, int Y)
{
    public static implicit operator Position((int X, int Y) tuple)
        => new (tuple.X, tuple.Y);

    public IEnumerable<(int X, int Y)> GetAdjacentPositions()
    {
        yield return (X - 1, Y);
        yield return (X + 1, Y);
        yield return (X, Y - 1);
        yield return (X, Y + 1);
    }
}
