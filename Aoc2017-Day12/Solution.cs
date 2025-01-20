namespace Aoc2017_Day12;

internal class Solution
{
    public string Title => "Day 12: Digital Plumber";

    public object PartOne()
    {
        var report = ConnectivityReport.Read();
        var group = FindConnectedGroup(report, program: 0);
        return group.Count;
    }

    public object PartTwo()
    {
        var report = ConnectivityReport.Read();
        var groupCount = 0;
        var unexamined = report.Keys.ToHashSet();
        while (unexamined.Count != 0)
        {
            var group = FindConnectedGroup(report, unexamined.First());
            unexamined.ExceptWith(group);
            groupCount++;
        }
        return groupCount;
    }

    private static HashSet<int> FindConnectedGroup(Dictionary<int, int[]> report, int program)
    {
        HashSet<int> set = [program];
        Stack<int> stack = new([program]);
        while (stack.Any())
        {
            var current = stack.Pop();
            foreach (var next in report[current])
            {
                if (set.Add(next))
                {
                    stack.Push(next);
                }
            }
        }
        return set;
    }
}
