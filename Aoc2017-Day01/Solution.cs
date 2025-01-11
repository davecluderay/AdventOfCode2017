namespace Aoc2017_Day01;

internal class Solution
{
    public string Title => "Day 1: Inverse Captcha";

    public object PartOne()
    {
        var captcha = InputFile.ReadAllText().TrimEnd();
        return Solve(captcha, offset: 1);
    }

    public object PartTwo()
    {
        var captcha = InputFile.ReadAllText().TrimEnd();
        return Solve(captcha, offset: captcha.Length / 2);
    }

    private static int Solve(string captcha, int offset)
    {
        var result = 0;
        for (var i = 0; i < captcha.Length; i++)
        {
            var d1 = captcha[i];
            var d2 = captcha[(i + offset) % captcha.Length];
            if (d1 == d2)
            {
                result += d1 - '0';
            }
        }
        return result;
    }
}
