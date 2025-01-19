using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2017_Day08;

internal readonly partial record struct Instruction(Mutation Mutation, Condition Condition)
{
    public static Instruction[] ReadAll()
        => InputFile.ReadAllLines()
                    .Select(Parse)
                    .ToArray();
    private static Instruction Parse(string text)
    {
        var match = Pattern.Match(text);
        if (!match.Success) throw new FormatException($"Unexpected instruction format: {text}");

        Mutation mutation = new(match.Groups["MutationRegister"].Value,
                                match.Groups["MutationType"].Value switch
                                {
                                    "inc" => MutationType.Inc,
                                    "dec" => MutationType.Dec,
                                    _     => throw new UnreachableException()
                                },
                                int.Parse(match.Groups["MutationAmount"].Value));

        Condition condition = new(match.Groups["ConditionRegister"].Value,
                                  match.Groups["ConditionType"].Value switch
                                  {
                                      "==" => ConditionType.Eq,
                                      "!=" => ConditionType.Neq,
                                      ">"  => ConditionType.Gt,
                                      ">=" => ConditionType.Gte,
                                      "<"  => ConditionType.Lt,
                                      "<=" => ConditionType.Lte,
                                      _    => throw new UnreachableException()
                                  },
                                  int.Parse(match.Groups["ConditionArgument"].Value));

        return new Instruction(mutation, condition);
    }

    [GeneratedRegex(@"^(?<MutationRegister>\w+)\s+(?<MutationType>inc|dec)\s+(?<MutationAmount>-?\d+)\s+" +
                    @"if\s+(?<ConditionRegister>\w+)\s+(?<ConditionType>==|!=|>|>=|<|<=)\s+(?<ConditionArgument>-?\d+)$")]
    private static partial Regex Pattern { get; }
}

internal readonly record struct Mutation(string Register, MutationType Type, int Amount);

internal enum MutationType { Inc, Dec }

internal readonly record struct Condition(string Register, ConditionType Type, int Argument); 

internal enum ConditionType { Eq, Neq, Gt, Gte, Lt, Lte }
