namespace Aoc2017_Day03;

internal readonly record struct Position(int X, int Y)
{
    public static implicit operator Position((int X, int Y) tuple)
        => new(tuple.X, tuple.Y);

    public static Position operator +(Position position, Vector vector)
        => new(position.X + vector.X, position.Y + vector.Y);

    public int ManhattanDistanceFrom(Position other)
        => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

    public IEnumerable<Position> GetAdjacentPositions()
    {
        yield return (X -1,  Y - 1);
        yield return (X -1,  Y + 0);
        yield return (X -1,  Y + 1);
        yield return (X + 0, Y - 1);
        yield return (X + 0, Y + 1);
        yield return (X + 1, Y - 1);
        yield return (X + 1, Y + 0);
        yield return (X + 1, Y + 1);
    }
}
