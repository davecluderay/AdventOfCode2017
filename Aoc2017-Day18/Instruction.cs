namespace Aoc2017_Day18;

internal abstract record Instruction
{
    public static Instruction Parse(string text)
    {
        var components = text.Split(' ');
        return components[0] switch
               {
                   "snd" => new Snd(RegisterNameOrValue.Parse(components[1])),
                   "set" => new Set(components[1].Single(), RegisterNameOrValue.Parse(components[2])),
                   "add" => new Add(components[1].Single(), RegisterNameOrValue.Parse(components[2])),
                   "mul" => new Mul(components[1].Single(), RegisterNameOrValue.Parse(components[2])),
                   "mod" => new Mod(components[1].Single(), RegisterNameOrValue.Parse(components[2])),
                   "rcv" => new Rcv(components[1].Single()),
                   "jgz" => new Jgz(RegisterNameOrValue.Parse(components[1]), RegisterNameOrValue.Parse(components[2])),
                   _     => throw new FormatException($"Unknown instruction: {text}")
               };
    }

    public sealed record Snd(RegisterNameOrValue X) : Instruction;
    public sealed record Set(char X, RegisterNameOrValue Y) : Instruction;
    public sealed record Add(char X, RegisterNameOrValue Y) : Instruction;
    public sealed record Mul(char X, RegisterNameOrValue Y) : Instruction;
    public sealed record Mod(char X, RegisterNameOrValue Y) : Instruction;
    public sealed record Rcv(char X) : Instruction;
    public sealed record Jgz(RegisterNameOrValue X, RegisterNameOrValue Y) : Instruction;
}
