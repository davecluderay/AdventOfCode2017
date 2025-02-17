namespace Aoc2017_Day21;

internal sealed class Rule
{
    private readonly char[] _pattern;
    private readonly char[] _replacement;

    private Rule(char[] pattern, char[] replacement)
    {
        _pattern = pattern;
        _replacement = replacement;
    }

    public bool IsMatch(char[] grid, int x, int y)
    {
        var applicableSize = (int)Math.Sqrt(grid.Length) % 2 == 0 ? 2 : 3;
        var patternSize = Size(_pattern);
        if (applicableSize != patternSize) return false;

        foreach (var orientation in MatchOrientations)
        {
            if (IsMatch(grid, x, y, orientation)) return true;
        }

        return false;
    }

    private bool IsMatch(char[] grid, int x, int y, int[] orientation)
    {
        var gridSize = (int)Math.Sqrt(grid.Length);
        var patternSize = Size(_pattern);
        for (var j = 0; j < patternSize; j++)
        for (var i = 0; i < patternSize; i++)
        {
            char patternChar = _pattern[orientation[i + j * patternSize]];
            char gridChar = grid[(y + j) * gridSize + x + i];
            if (patternChar != gridChar) return false;
        }
        return true;
    }

    public void Apply(char[] grid, int x, int y)
    {
        var gridSize = (int)Math.Sqrt(grid.Length);
        var replacementSize = Size(_replacement);

        for (var j = 0; j < replacementSize; j++)
        for (var i = 0; i < replacementSize; i++)
        {
            grid[x + i + (y + j) * gridSize] = _replacement[i + j * replacementSize];
        }
    }

    private int[][] MatchOrientations
        => Size(_pattern) == 2
               ? MatchOrientationsSize2
               : MatchOrientationsSize3;

    private int Size(char[] template) => (int)Math.Sqrt(template.Length);

    public static Rule Parse(string input)
    {
        var parts = input.Split(" => ");
        var pattern = parts[0].Where(c => c != '/').ToArray();
        var replacement = parts[1].Where(c => c != '/').ToArray();
        return new Rule(pattern, replacement);
    }

    private static readonly int[][] MatchOrientationsSize2 =
    [
        [0, 1, 2, 3],
        [2, 3, 0, 1],
        [1, 0, 3, 2],
        [3, 2, 1, 0],
        [0, 2, 1, 3],
        [1, 3, 0, 2],
        [2, 0, 3, 1],
        [3, 1, 2, 0]
    ];

    private static readonly int[][] MatchOrientationsSize3 =
    [
        [0, 1, 2, 3, 4, 5, 6, 7, 8],
        [2, 1, 0, 5, 4, 3, 8, 7, 6],
        [6, 7, 8, 3, 4, 5, 0, 1, 2],
        [8, 7, 6, 5, 4, 3, 2, 1, 0],
        [0, 3, 6, 1, 4, 7, 2, 5, 8],
        [6, 3, 0, 7, 4, 1, 8, 5, 2],
        [2, 5, 8, 1, 4, 7, 0, 3, 6],
        [8, 5, 2, 7, 4, 1, 6, 3, 0]
    ];
}
