namespace Aoc2017_Day16;

internal class Solution
{
    public string Title => "Day 16: Permutation Promenade";

    public object PartOne()
    {
        var (programs, moves) = ReadInput(); 
        foreach (var move in moves)
        {
            Move(programs, move);
        }
        return new string(programs);
    }

    public object PartTwo()
    {
        var (programs, moves) = ReadInput();

        HashSet<string> seen = new();
        List<string> sequence = new();
        for (var n = 0; n < 1_000_000_000; n++)
        {
            foreach (var move in moves)
            {
                Move(programs, move);
            }

            var snapshot = new string(programs);
            if (!seen.Add(snapshot))
            {
                // The pattern has hit a cycle, so extrapolate to the end.
                var seenAt = sequence.IndexOf(snapshot);
                var offset = seenAt + (1_000_000_000 - seenAt) % (n - seenAt) - 1;
                return sequence[offset];
            }

            sequence.Add(snapshot);
        }

        // No cycles found (unexpected).
        return new string(programs);
    }

    private static void Move(Span<char> programs, ReadOnlySpan<char> move)
    {
        switch (move[0])
        {
            case 's':
            {
                var count = int.Parse(move[1..]);
                Span<char> temp = stackalloc char[count];
                programs[^count..].CopyTo(temp);
                programs[..^count].CopyTo(programs[count..]);
                temp.CopyTo(programs);
                break;
            }
            case 'x':
            {
                var i = move.IndexOf('/');
                var x = int.Parse(move[1..i]);
                var y = int.Parse(move[(i + 1)..]);
                (programs[x], programs[y]) = (programs[y], programs[x]);
                break;
            }
            case 'p':
            {
                var x = programs.IndexOf(move[1]);
                var y = programs.IndexOf(move[3]);
                (programs[x], programs[y]) = (programs[y], programs[x]);
                break;
            }
        }
    }

    private static (char[] Programs, string[] moves) ReadInput()
    {
        var programs = Enumerable.Range('a', 16).Select(i => (char)i).ToArray();
        var moves = InputFile.ReadAllText().TrimEnd().Split(',').ToArray();
        return (programs, moves);
    }
}
