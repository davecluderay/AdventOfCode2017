
using System.Diagnostics;

namespace Aoc2017_Day07;

internal class Solution
{
    public string Title => "Day 7: Recursive Circus";

    public object PartOne()
    {
        var programs = TowerProgram.ReadAll();
        var bottomProgram = FindBottomProgram(programs);
        return bottomProgram.Name;
    }

    public object PartTwo()
    {
        var programs = TowerProgram.ReadAll();
        var (_, correctedWeight) = FindProgramWithIncorrectWeight(programs);
        return correctedWeight;
    }

    private static TowerProgram FindBottomProgram(TowerProgram[] programs)
    {
        var nameSet = programs.Select(p => p.Name).ToHashSet();
        foreach (var d in programs.SelectMany(p => p.Dependants))
        {
            nameSet.Remove(d);
        }
        var rootName = nameSet.Single();
        return programs.First(p => p.Name == rootName);
    }

    // ReSharper disable once UnusedTupleComponentInReturnValue
    private static (TowerProgram Program, int CorrectWeight) FindProgramWithIncorrectWeight(TowerProgram[] programs)
    {
        var bottomProgram = FindBottomProgram(programs);
        var programDictionary = programs.ToDictionary(p => p.Name);

        // Build cache of total weights for every program.
        var weightCache = new Dictionary<string, (int Weight, int Level)>();
        CalculateWeight(bottomProgram.Name, programDictionary, 0, weightCache);

        // Find the highest program that is incorrectly balanced.
        var highestUnbalanced = programs.OrderByDescending(p => weightCache[p.Name].Level)
                                        .First(program => program.Dependants
                                                                 .GroupBy(d => weightCache[d].Weight)
                                                                 .Count() > 1);

        // Find whichever dependant program is causing the imbalance (by being weighted incorrectly).
        var groups = highestUnbalanced.Dependants
                            .GroupBy(d => weightCache[d].Weight)
                            .OrderBy(g => g.Count())
                            .ToArray();
        if (groups.Length != 2) throw new UnreachableException($"Expected two weight groups, but found {groups.Length}.");
        if (groups[0].Count() != 1) throw new UnreachableException($"Expected smaller weight group to have 1 item, but found {groups[0].Count()}.");

        // Return the incorrectly weighted program, along with its correct weight. 
        var weightDifference = groups[0].Key - groups[1].Key;
        return (programDictionary[groups[0].Single()], programDictionary[groups[0].Single()].Weight - weightDifference);

        static int CalculateWeight(string programName, Dictionary<string, TowerProgram> programs, int level, Dictionary<string, (int Weight, int Level)> weightCache)
        {
            if (weightCache.TryGetValue(programName, out var cached))
            {
                return cached.Weight;
            }

            var program = programs[programName];
            var childWeights = program.Dependants.Select(dependant => CalculateWeight(dependant, programs, level + 1, weightCache))
                                      .ToArray();
            var totalWeight = childWeights.Sum() + program.Weight;
            weightCache[programName] = (totalWeight, level);
            return totalWeight;
        }
    }
}
