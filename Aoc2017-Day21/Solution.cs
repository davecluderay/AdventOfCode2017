namespace Aoc2017_Day21;

internal class Solution
{
    public string Title => "Day 21: Fractal Art";

    public object PartOne()
    {
        var (rules, grid) = ReadInput();
        grid = Iterate(grid, rules, 5);
        return grid.Count(c => c == '#');
    }

    public object PartTwo()
    {
        var (rules, grid) = ReadInput();
        grid = Iterate(grid, rules, 18);
        return grid.Count(c => c == '#');
    }

    private static char[] Iterate(char[] grid, Rule[] rules, int iterationCount)
    {
        for (var iterations = 0; iterations < iterationCount; iterations++)
        {
            var gridSize = (int)Math.Sqrt(grid.Length);
            var matchSize = gridSize % 2 == 0 ? 2 : 3;
            var replacementSize = matchSize + 1;
            var nextGridSize = gridSize * replacementSize / matchSize;

            var nextGrid = new char[nextGridSize * nextGridSize];
            for (var y = 0; y < gridSize; y += matchSize)
            for (var x = 0; x < gridSize; x += matchSize)
            {
                var matchingRule = rules.First(r => r.IsMatch(grid, x, y));
                matchingRule.Apply(nextGrid, x / matchSize * replacementSize, y / matchSize * replacementSize);
            }
            grid = nextGrid;
        }
        return grid;
    }

    private static (Rule[] Rules, char[] InitialGrid) ReadInput()
    {
        var rules = InputFile.ReadAllLines()
                             .Select(Rule.Parse)
                             .ToArray();
        char[] initialGrid = ['.', '#', '.', '.', '.', '#', '#', '#', '#'];
        return (rules, initialGrid);
    }
}
