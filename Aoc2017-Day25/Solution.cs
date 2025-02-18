namespace Aoc2017_Day25;

internal class Solution
{
    public string Title => "Day 25: The Halting Problem";

    public object PartOne()
    {
        var rules = Rules.Read();

        HashSet<int> tape = new();
        var position = 0;
        var state = rules.States[rules.StartState];

        for (var n = 0; n < rules.DiagnosticSteps; n++)
        {
            var stateRule = tape.Contains(position) ? state.WhenOne : state.WhenZero;

            if (stateRule.WriteValue == 1)
                tape.Add(position);
            else
                tape.Remove(position);

            position += stateRule.Move;
            state = rules.States[stateRule.NextState];
        }

        return tape.Count;
    }

    public object PartTwo()
    {
        return "MERRY CHRISTMAS!";
    }
}
