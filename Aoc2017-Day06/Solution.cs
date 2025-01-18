namespace Aoc2017_Day06;

internal class Solution
{
    public string Title => "Day 6: Memory Reallocation";

    public object PartOne()
    {
        var initialBanks = ReadMemoryBanks();
        var (steps, _) = FindNextRepetition(initialBanks);
        return steps;
    }

    public object PartTwo()
    {
        var initialBanks = ReadMemoryBanks();
        var (_, banks) = FindNextRepetition(initialBanks);
        var (steps, _) = FindNextRepetition(banks);
        return steps;
    }

    private (int Steps, int[] MemoryBanks) FindNextRepetition(int[] banks)
    {
        banks = banks.ToArray();

        HashSet<string> seen = [string.Join(',', banks)];
        int steps = 0;
        while (true)
        {
            steps++;
            var selectedBank = banks.Select((blocks, index) => (blocks, index))
                                    .OrderByDescending(x => x.blocks)
                                    .ThenBy(x => x.index)
                                    .Select(x => x.index)
                                    .First();

            var blocks = banks[selectedBank];
            banks[selectedBank] = 0;

            var redistributeIndex = (selectedBank + 1) % banks.Length;
            while (blocks > 0)
            {
                banks[redistributeIndex]++;
                blocks--;
                redistributeIndex = (redistributeIndex + 1) % banks.Length;
            }

            if (!seen.Add(string.Join(',', banks)))
            {
                return (steps, banks);
            }
        }
    }

    private static int[] ReadMemoryBanks()
        => InputFile.ReadAllText()
                    .Split('\t', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
}
