namespace Aoc2017_Day20;

internal class Solution
{
    public string Title => "Day 20: Particle Swarm";

    public object PartOne()
    {
        var particles = Particle.ReadAll();
        var particle = particles.MinBy(p => p.CalculatePosition(1000)
                                             .CalculateDistanceFromZero())!;
        return particle.Id;
    }

    public object PartTwo()
    {
        var particles = Particle.ReadAll();
        var remaining = particles.Select(p => p.Id).ToHashSet();
        for (var t = 0; t <= 1000; t++)
        {
            var collidedIds = particles.Where(p => remaining.Contains(p.Id))
                                       .GroupBy(p => p.CalculatePosition(t))
                                       .Where(g => g.Count() > 1)
                                       .SelectMany(g => g.Select(x => x.Id));
            remaining.ExceptWith(collidedIds);
        }
        return remaining.Count;
    }
}
