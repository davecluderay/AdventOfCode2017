using System.Text;

namespace Aoc2017_Day14;

internal static class KnotHash
{
    public static string Calculate(string key)
    {
        var keyData = key.Select(b => (int)b)
                         .Concat([17, 31, 73, 47, 23])
                         .ToArray();

        var list = Enumerable.Range(0, 256).ToArray();
        Hash(list, keyData, rounds: 64);

        return ToDenseHashHexString(list);
    }

    private static void Hash(int[] list, int[] lengths, int rounds)
    {
        var index = 0;
        var skip = 0;
        for (var round = 0; round < rounds; round++)
        {
            foreach (var length in lengths)
            {
                for (var i = 0; i < length / 2; i++)
                {
                    var a = (index + i) % list.Length;
                    var b = (index + length - i - 1) % list.Length;
                    (list[a], list[b]) = (list[b], list[a]);
                }
                index = (index + length + skip) % list.Length;
                skip++;
            }
        }
    }

    private static string ToDenseHashHexString(int[] list)
    {
        if (list.Length != 256) throw new ArgumentOutOfRangeException(nameof(list), list.Length, "Unexpected length.");

        var denseHash = new int[16];
        for (var segment = 0; segment < 16; segment++)
        for (var offset = 0; offset < 16; offset++)
        {
            denseHash[segment] ^= list[segment * 16 + offset];
        }

        return denseHash.Aggregate(new StringBuilder(32),
                                   (builder, @byte) => builder.Append($"{@byte:x2}"),
                                   builder => builder.ToString());
    }
}
