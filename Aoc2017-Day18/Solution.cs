namespace Aoc2017_Day18;

internal class Solution
{
    public string Title => "Day 18: Duet";

    public object PartOne()
    {
        var instance = new ProgramInstance(0, ReadProgram());
        instance.RunUntilBlockedOrComplete([]);
        return instance.OutputQueue.ToArray()[^1];
    }

    public object PartTwo()
    {
        var program = ReadProgram();

        var instance0 = new ProgramInstance(0, program);
        var instance1 = new ProgramInstance(1, program);
        do
        {
            instance0.RunUntilBlockedOrComplete(instance1.OutputQueue);
            instance1.RunUntilBlockedOrComplete(instance0.OutputQueue);
        } while (instance0.OutputQueue.Count > 0 || instance1.OutputQueue.Count > 0);

        return instance1.SendCount;
    }

    private static Instruction[] ReadProgram()
    {
        return InputFile.ReadAllLines()
                        .Select(Instruction.Parse)
                        .ToArray();
    }
}
