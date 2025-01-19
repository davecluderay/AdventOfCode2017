using System.Diagnostics;

namespace Aoc2017_Day08;

internal class Solution
{
    public string Title => "Day 8: I Heard You Like Registers";

    public object PartOne()
    {
        var instructions = Instruction.ReadAll();

        Dictionary<string, int> registers = new();
        foreach (var (mutation, condition) in instructions)
        {
            if (IsConditionMet(registers, condition))
            {
                Apply(registers, mutation);
            }
        }

        return registers.Values.Max();
    }

    public object PartTwo()
    {
        var instructions = Instruction.ReadAll();

        var maxValue = 0;
        Dictionary<string, int> registers = new();
        foreach (var (mutation, condition) in instructions)
        {
            if (IsConditionMet(registers, condition))
            {
                var (_, value) = Apply(registers, mutation);
                maxValue = Math.Max(maxValue, value);
            }
        }

        return maxValue;
    }

    private bool IsConditionMet(Dictionary<string,int> registers, Condition condition)
    {
        var (register, type, argument) = condition;
        var registerValue = registers.GetValueOrDefault(register);
        return type switch
               {
                   ConditionType.Eq  => registerValue == argument,
                   ConditionType.Neq => registerValue != argument,
                   ConditionType.Gt  => registerValue > argument,
                   ConditionType.Gte => registerValue >= argument,
                   ConditionType.Lt  => registerValue < argument,
                   ConditionType.Lte => registerValue <= argument,
                   _                 => throw new UnreachableException()
               };
    }

    // ReSharper disable once UnusedTupleComponentInReturnValue
    private (string Register, int Value) Apply(Dictionary<string, int> registers, Mutation mutation)
    {
        var (register, type, amount) = mutation;
        var registerValue = registers.GetValueOrDefault(register);
        registers[register] = type switch
                              {
                                  MutationType.Inc => registerValue + amount,
                                  MutationType.Dec => registerValue - amount,
                                  _                => throw new UnreachableException()
                              };
        return (register, registers[register]);
    }
}
