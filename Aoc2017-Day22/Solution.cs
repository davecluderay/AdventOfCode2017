namespace Aoc2017_Day22;

internal class Solution
{
    public string Title => "Day 22: Sporifica Virus";

    public object PartOne() => CountInfectionsCaused(burstCount: 10_000, evolved: false);

    public object PartTwo() => CountInfectionsCaused(burstCount: 10_000_000, evolved: true);

    private static int CountInfectionsCaused(int burstCount, bool evolved)
    {
        var infectionsCaused = 0;

        var (position, orientation, nodes) = ReadInput();
        for (var i = 0; i < burstCount; i++)
        {
            var currentState = nodes.GetValueOrDefault(position, '.');
            var nextState = currentState switch
                            {
                                '.' => evolved ? 'W' : '#',
                                'W' => '#',
                                '#' => evolved ? 'F' : '.',
                                'F' => '.',
                                _   => throw new InvalidOperationException()
                            };

            if (nextState == '.') nodes.Remove(position);
            else nodes[position] = nextState;
            
            if (nextState == '#') infectionsCaused++;

            orientation = currentState switch
                          {
                              '.' => orientation.TurnLeft(),
                              'W' => orientation,
                              '#' => orientation.TurnRight(),
                              'F' => orientation.TurnRight().TurnRight(),
                              _   => throw new InvalidOperationException()
                          };
            position += orientation;
        }

        return infectionsCaused;
    }

    private static (Vector Position, Vector Orientation, Dictionary<Vector, char> Infected) ReadInput()
    {
        var lines = InputFile.ReadAllLines();

        Dictionary<Vector, char> infected = [];
        for (var y = 0; y < lines.Length; y++)
        for (var x = 0; x < lines[y].Length; x++)
        {
            if (lines[y][x] == '#')
            {
                infected[(x, y)] = '#';
            }
        }

        Vector position = (lines[0].Length / 2, lines.Length / 2);
        Vector orientation = (0, -1);

        return (position, orientation, infected);
    }
}
