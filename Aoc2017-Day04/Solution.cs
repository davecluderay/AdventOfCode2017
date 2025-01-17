namespace Aoc2017_Day04;

internal class Solution
{
    public string Title => "Day 4: High-Entropy Passphrases";

    public object PartOne()
        => InputFile.ReadAllLines()
                    .Count(l => IsValidPassphrase(l, TransformWord));

    public object PartTwo()
        => InputFile.ReadAllLines()
                    .Count(l => IsValidPassphrase(l, TransformWordWithAlphabeticSort));

    private static bool IsValidPassphrase(ReadOnlySpan<char> candidate, Func<ReadOnlySpan<char>, string> transformWord)
    {
        var words = candidate.Split(' ');
        HashSet<string> set = new();
        foreach (var word in words)
        {
            if (!set.Add(transformWord(candidate[word])))
            {
                return false;
            }
        }
        return true;
    }

    private static string TransformWord(ReadOnlySpan<char> word)
    {
        return new string(word);
    }

    private static string TransformWordWithAlphabeticSort(ReadOnlySpan<char> word)
    {
        var chars = word.ToArray();
        Array.Sort(chars);
        return new string(chars);
    }
}
