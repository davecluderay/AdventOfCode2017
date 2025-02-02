namespace Aoc2017_Day19;

internal class RouteLayout
{
    private readonly string[] _lines;

    public Vector Start { get; }

    private RouteLayout(string[] lines)
    {
        _lines = lines;
        Start = (_lines[0].IndexOf('|'), 0);
    }

    public bool IsTurningPoint(Vector position)
        => _lines[position.Y][position.X] == '+';

    public bool IsSpace(Vector position)
        => _lines[position.Y][position.X] == ' ';

    public char? LetterAt(Vector position)
    {
        var ch = _lines[position.Y][position.X];
        return char.IsLetter(ch) ? ch : null;
    }

    public static RouteLayout Read()
    {
        var lines = InputFile.ReadAllLines();
        return new RouteLayout(lines);
    }
}
