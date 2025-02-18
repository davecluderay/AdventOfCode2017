namespace Aoc2017_Day24;

internal record Component(int Port1, int Port2)
{
    public int Strength => Port1 + Port2;

    public static Component Parse(string text)
    {
        var ports = text.Split('/')
                        .Select(int.Parse)
                        .Order()
                        .ToArray();
        return ports switch
               {
                   [var a, var b] => new Component(a, b),
                   _              => throw new ArgumentException($"Unexpected value: {text}", nameof(text))
               };

    }
}
