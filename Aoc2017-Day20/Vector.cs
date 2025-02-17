namespace Aoc2017_Day20;

internal readonly record struct Vector(long X, long Y, long Z)
{
    public static implicit operator Vector((long X, long Y, long Z) tuple)
        => new(tuple.X, tuple.Y, tuple.Z);

    public static Vector operator -(Vector a, Vector b)
        => new(a.X - b.X,
               a.Y - b.Y,
               a.Z - b.Z);

    public static Vector operator +(Vector a, Vector b)
        => new(a.X + b.X,
               a.Y + b.Y,
               a.Z + b.Z);

    public static Vector operator /(Vector v, long n)
        => new(v.X / n,
               v.Y / n,
               v.Z / n);

    public static Vector operator *(long n, Vector v)
        => new(v.X * n, v.Y * n, v.Z * n);

    public long CalculateDistanceFromZero()
        => Math.Abs(X) +
           Math.Abs(Y) +
           Math.Abs(Z);
}
