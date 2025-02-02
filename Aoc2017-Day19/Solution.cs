using System.Text;

namespace Aoc2017_Day19;

internal class Solution
{
    public string Title => "Day 19: A Series of Tubes";

    public object PartOne()
    {
        var routeLayout = RouteLayout.Read();
        return Follow(routeLayout).Letters;
    }

    public object PartTwo()
    {
        var routeLayout = RouteLayout.Read();
        return Follow(routeLayout).StepsTaken;
    }

    private static (string Letters, int StepsTaken) Follow(RouteLayout routeLayout)
    {
        var letters = new StringBuilder();
        var stepsTaken = 1;

        Vector at = routeLayout.Start;
        Vector direction = (0, 1);
        while (true)
        {
            Vector[] directions = routeLayout.IsTurningPoint(at)
                                              ? [direction, direction.RotateLeft(), direction.RotateRight()]
                                              : [direction];
            var hasMoved = false;
            foreach (var nextDirection in directions)
            {
                var next = at + nextDirection;
                if (!routeLayout.IsSpace(next))
                {
                    var letter = routeLayout.LetterAt(next);
                    if (letter.HasValue) letters.Append(letter.Value);

                    (at, direction) = (next, nextDirection);
                    hasMoved = true;
                    break;
                }
            }

            if (!hasMoved) break;
            stepsTaken++;
        }

        return (letters.ToString(), stepsTaken);
    }
}
