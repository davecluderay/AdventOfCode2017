namespace Aoc2017_Day09;

internal class Solution
{
    public string Title => "Day 9: Stream Processing";

    public object PartOne()
    {
        var stream = InputFile.ReadAllText().Trim();
        var (totalScore, _) = ProcessStream(stream);
        return totalScore;
    }

    public object PartTwo()
    {
        var stream = InputFile.ReadAllText().Trim();
        var (_, totalGarbage) = ProcessStream(stream);
        return totalGarbage;
    }

    private (int TotalScore, int TotalGarbage) ProcessStream(string stream)
    {
        var totalScore = 0;
        var groupScore = 0;
        var totalGarbage = 0;
        var inGarbage = false;
        for (var i = 0; i < stream.Length; i++)
        {
            switch (stream[i])
            {
                case '{':
                    if (inGarbage)
                    {
                        totalGarbage++;
                    }
                    else
                    {
                        groupScore++;
                        totalScore += groupScore;
                    }
                    break;
                case '}':
                    if (inGarbage)
                    {
                        totalGarbage++;
                    }
                    else
                    {
                        groupScore--;
                    }
                    break;
                case '<':
                    if (inGarbage)
                    {
                        totalGarbage++;
                    }
                    else
                    {
                        inGarbage = true;
                    }
                    break;
                case '!':
                    if (inGarbage)
                    {
                        i++;
                    }
                    break;
                case '>':
                    if (inGarbage)
                    {
                        inGarbage = false;
                    }
                    break;
                default:
                    if (inGarbage)
                    {
                        totalGarbage++;
                    } 
                    break;
            }
        }
        return (totalScore, totalGarbage);
    }
}
