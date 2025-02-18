namespace Aoc2017_Day23;

internal class Solution
{
    public string Title => "Day 23: Coprocessor Conflagration";

    public object PartOne()
    {
        var program = ReadProgram();
        var instance = new ProgramInstance(0, program);
        instance.RunUntilComplete();
        return instance.MulCount;
    }

    public object PartTwo()
    {
        // From reverse engineering the program, it is checking a particular set of numbers
        // and counting which are non-prime. The numbers in the set increase by 17 each time.
        // The starting number is input-specific, based on the second operand from the first
        // instruction in the program.
        var firstInstruction = (Instruction.Set)ReadProgram().First();
        var specialNumber = firstInstruction.Y.AsValue;
        var baseNumber = specialNumber * 100 + 100_000;
        return Enumerable.Range(0, 1001)
                         .Count(n => !IsPrime(baseNumber + n * 17));
    }

    // https://stackoverflow.com/a/44203452/30933
    private static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2 || number == 3 || number == 5) return true;
        if (number % 2 == 0 || number % 3 == 0 || number % 5 == 0) return false;

        var boundary = (int)Math.Floor(Math.Sqrt(number));
        for (int i = 6; i <= boundary; i += 6)
        {
            if (number % (i + 1) == 0 || number % (i + 5) == 0)
                return false;
        }

        return true;
    }

    private static Instruction[] ReadProgram()
    {
        return InputFile.ReadAllLines()
                        .Select(Instruction.Parse)
                        .ToArray();
    }
}
