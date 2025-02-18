using System.Collections.Immutable;

namespace Aoc2017_Day24;

internal class Solution
{
    public string Title => "Day 24: Electromagnetic Moat";

    public object PartOne()
        => EvaluateBridges(ReadComponents(), BridgeEvaluation.PreferringStrength());

    public object PartTwo()
        => EvaluateBridges(ReadComponents(), BridgeEvaluation.PreferringLength());

    private int EvaluateBridges(Component[] components, BridgeEvaluation best)
    {
        Evaluate(0, 0, 0, components.ToImmutableHashSet(), best);
        return best.GetBestStrength();

        static void Evaluate(int port, int length, int strength, ImmutableHashSet<Component> remaining, BridgeEvaluation best)
        {
            var candidates = remaining.Where(c => c.Port1 == port || c.Port2 == port)
                                      .ToArray();
            if (candidates.Any())
            {
                foreach (var candidate in candidates)
                {
                    var nextPort = candidate.Port1 == port ? candidate.Port2 : candidate.Port1;
                    Evaluate(nextPort, length + 1, strength + candidate.Strength, remaining.Remove(candidate), best);
                }
            }
            else
            {
                best.Evaluate(length, strength);
            }
        }
    }

    private static Component[] ReadComponents()
        => InputFile.ReadAllLines()
                    .Select(Component.Parse)
                    .ToArray();

    private class BridgeEvaluation(bool preferLength)
    {
        private int _bestLength;
        private int _bestStrength;

        public void Evaluate(int length, int strength)
        {
            bool shouldApply = preferLength
                ? length > _bestLength || (length == _bestLength && strength > _bestStrength)
                : strength > _bestStrength || (strength == _bestStrength && length > _bestLength);
            if (!shouldApply) return;

            _bestLength = length;
            _bestStrength = strength;
        }

        public int GetBestStrength() => _bestStrength;

        public static BridgeEvaluation PreferringLength() => new(preferLength: true);
        public static BridgeEvaluation PreferringStrength() => new(preferLength: false);
    }
}
