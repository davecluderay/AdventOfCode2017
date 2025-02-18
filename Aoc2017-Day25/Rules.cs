using System.Text.RegularExpressions;

namespace Aoc2017_Day25;

internal class Rules
{
    public char StartState { get; }
    public int DiagnosticSteps { get; }
    public IDictionary<char, State> States { get; }

    private Rules(char startState, int diagnosticSteps, IDictionary<char, State> states)
    {
        StartState = startState;
        DiagnosticSteps = diagnosticSteps;
        States = states;
    }

    public static Rules Read()
    {
        var sections = InputFile.ReadInSections().ToArray();
        var (startState, diagnosticSteps) = ReadHeader(sections[0]);
        var states = sections.Skip(1).Select(ReadState).ToDictionary(s => s.Id);
        return new(startState, diagnosticSteps, states);
    }

    private static (char StartState, int DiagnosticSteps) ReadHeader(string[] section)
    {
        var match = Regex.Match(section[0], @"Begin in state (?<StartState>\w).");
        if (!match.Success) throw new FormatException($"Unable to read start state from: {section[0]}");
        var startState = match.Groups["StartState"].Value.Single();

        match = Regex.Match(section[1], @"Perform a diagnostic checksum after (?<DiagnosticSteps>\d+) steps.");
        if (!match.Success) throw new FormatException($"Unable to read diagnostic steps from: {section[1]}");
        var diagnosticSteps = int.Parse(match.Groups["DiagnosticSteps"].Value);

        return (startState, diagnosticSteps);
    }

    private static State ReadState(string[] section)
    {
        var match = Regex.Match(section[0], @"In state (?<Id>\w):");
        if (!match.Success) throw new FormatException($"Unable to read state ID from: {section[0]}");
        var id = match.Groups["Id"].Value.Single();

        var whenZero = ReadStateRule(section[2..5]);
        var whenOne = ReadStateRule(section[6..9]);

        return new(id, whenZero, whenOne);
    }

    private static StateRule ReadStateRule(string[] subsection)
    {
        var match = Regex.Match(subsection[0], @"Write the value (?<WriteValue>\d).");
        if (!match.Success) throw new FormatException($"Unable to read the value to be written from: {subsection[0]}");
        var writeValue = int.Parse(match.Groups["WriteValue"].Value);

        match = Regex.Match(subsection[1], @"Move one slot to the (?<Direction>right|left).");
        if (!match.Success) throw new FormatException($"Unable to read the direction to move from: {subsection[1]}");
        var move = match.Groups["Direction"].Value == "right" ? 1 : -1;

        match = Regex.Match(subsection[2], @"Continue with state (?<NextState>\w).");
        if (!match.Success) throw new FormatException($"Unable to read the next state from: {subsection[2]}");
        var nextState = match.Groups["NextState"].Value.Single();

        return new(writeValue, move, nextState);
    }
}


internal record State(char Id, StateRule WhenZero, StateRule WhenOne);
internal record StateRule(int WriteValue, int Move, char NextState);
